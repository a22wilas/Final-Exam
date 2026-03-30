import http from 'k6/http';
import { SharedArray } from 'k6/data';

// Load JSON data once
const data = new SharedArray('data', function () {
  return JSON.parse(open('../JSON/data.json'));
});

// Test configuration
export const options = {
  vus: 1,
  iterations: data.length,
  insecureSkipTLSVerify: true,
};

export default function () {
  const item = data[__ITER];
  const id = __ITER;


  const payloadObj = {
    id: id,
    ...item
  };

  const payload = JSON.stringify(payloadObj);

  const startTime = new Date().toISOString();

  const res = http.post('http://localhost:5178/messages', payload, {
    headers: { 'Content-Type': 'application/json' },
  });

  let responseBody = {};
  try {
    responseBody = res.json();
  } catch (e) {
    responseBody = {};
  }

  console.log(JSON.stringify({
    id: id,
    sentAt: startTime,
    status: res.status,
    receivedAt: responseBody.receivedAt || null,
    payload: payloadObj
  }));
}