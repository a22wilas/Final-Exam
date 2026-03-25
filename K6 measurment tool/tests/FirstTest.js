import http from 'k6/http';
import { check, sleep } from 'k6';

// Configurable options
export const options = {
  vus: 10,              // virtual users
  duration: '30s',      // test duration
  thresholds: {
    http_req_duration: ['p(95)<500'], // 95% under 500ms
    http_req_failed: ['rate<0.01'],   // <1% errors
  },
};

export default function () {
  const res = http.get(__ENV.BASE_URL || 'https://test.k6.io');

  check(res, {
    'status is 200': (r) => r.status === 200,
  });

  sleep(1);
}