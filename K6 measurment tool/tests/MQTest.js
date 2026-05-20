import http from 'k6/http';
import { SharedArray } from 'k6/data';
import encoding from 'k6/encoding';

const VUS = 50;

const data = new SharedArray('data', function () {
  return JSON.parse(open('../JSON/data.json'));
});

export const options = {
  vus: VUS,
  iterations: data.length * VUS,
  insecureSkipTLSVerify: true,
};

// Base64 encode credentials for Basic Auth
const credentials = encoding.b64encode('guest:guest');

export default function () {
  const index = (__ITER % data.length);
  const item = data[index];
  const id = __ITER;
  const sentAt = new Date().toISOString();

  const payloadObj = JSON.stringify({
    id: id,
    ...item,
    SentAt: sentAt
  });

    const payload = JSON.stringify({
    properties: {},
    routing_key: "logistic_mq_messages",
    payload: encoding.b64encode(payloadObj),
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