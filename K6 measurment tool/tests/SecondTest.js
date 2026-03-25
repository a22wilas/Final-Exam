import http from 'k6/http';

// Load once (shared across VUs)
const data = JSON.parse(open('../JSON/data.json'));
console.log(open('../JSON/data.json'));

export default function () {
  const item = data[Math.floor(Math.random() * data.length)];

  const payload = JSON.stringify({
    id: __VU + "-" + __ITER,
    ...item
  });

  http.post('https://your-api.com/endpoint', payload, {
    headers: { 'Content-Type': 'application/json' },
  });
}