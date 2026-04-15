import http from 'k6/http';
import { SharedArray } from 'k6/data';
import encoding from 'k6/encoding';

const data = new SharedArray('data', function () {
  return JSON.parse(open('../JSON/data.json'));
});

export const options = {
  vus: 1,
  iterations: data.length,
  insecureSkipTLSVerify: true,
};

// Base64 encode credentials for Basic Auth
const credentials = encoding.b64encode('guest:guest');

export default function () {
  const item = data[__ITER];
  const sentAt = new Date().toISOString();

  const messageBody = JSON.stringify({
    ...item,
    SentAt: sentAt
  });

  // RabbitMQ HTTP API requires the body to be base64 encoded
  const payload = JSON.stringify({
    properties: {},
    routing_key: "logistic_mq_messages",
    payload: encoding.b64encode(messageBody),
    payload_encoding: "base64"
  });

  const res = http.post(
    'http://localhost:15672/api/exchanges/%2F/amq.default/publish',
    payload,
    {
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Basic ${credentials}`
      }
    }
  );

  console.log(JSON.stringify({
    iter: __ITER,
    sentAt: sentAt,
    status: res.status,
    routed: res.json().routed || false
  }));
}