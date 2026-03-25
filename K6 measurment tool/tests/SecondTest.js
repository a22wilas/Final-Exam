import http from 'k6/http';
import { SharedArray } from 'k6/data';

// Load data once
const data = new SharedArray('my data', function () {
  return JSON.parse(open('../JSON/data.json'));
});

export const options = {
  vus: 1,                         // only 1 user
  iterations: data.length,        // run once per data item
};

export default function () {
  const item = data[__ITER];      // 👈 key change

  const id = __ITER;

  const payloadObj = {
    id: id,
    ...item
  };

  const payload = JSON.stringify(payloadObj);

  const res = http.post('https://jsonplaceholder.typicode.com/posts', payload, {
    headers: { 'Content-Type': 'application/json' },
  });

  console.log(JSON.stringify({
    id: id,
    status: res.status,
    payload: payloadObj
  }));
}