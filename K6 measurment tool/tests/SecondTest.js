import http from 'k6/http';
import { SharedArray } from 'k6/data';

// Load JSON data once
const data = new SharedArray('data', function () {
  return JSON.parse(open('../JSON/data.json'));
});

// Test configuration
export const options = {
  vus: 1,                      // start simple (no duplicates)
  iterations: data.length,     // send each item once
  insecureSkipTLSVerify: true, // ignore HTTPS cert issues (localhost)
};

export default function () {
  const item = data[__ITER];   // one item per iteration
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

  // ✅ Structured logging (for your thesis)
  console.log(JSON.stringify({
    id: id,
    sentAt: startTime,
    status: res.status,
    receivedAt: responseBody.receivedAt || null,
    payload: payloadObj
  }));
}