import http from 'k6/http';
import { SharedArray } from 'k6/data';


const data = new SharedArray('data', function () {
  return JSON.parse(open('../JSON/data.json'));
});

export const options = {
  vus: 1,
  iterations: data.length,
  insecureSkipTLSVerify: true,
};

export default function () {
  const index = (__ITER % data.length);
  const item = data[index];
  const id = __ITER;
  const sentAt = new Date().toISOString(); 

  const payloadObj = {
    id: id,
    ...item,
    SentAt: sentAt   
  };

  const payload = JSON.stringify(payloadObj);

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
    sentAt: sentAt,
    status: res.status,
    receivedAt: responseBody.receivedAt || null,
    payload: payloadObj
  }));
}