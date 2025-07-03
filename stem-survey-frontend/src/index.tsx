import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import * as serviceWorkerRegistration from './serviceWorkerRegistration';
import reportWebVitals from './reportWebVitals';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);

// Якщо ви хочете, щоб ваша програма працювала офлайн і завантажувалася швидше, ви можете змінити
// unregister() на register() нижче. Зверніть увагу, що це має деякі недоліки.
// Дізнайтеся більше про service workers: https://cra.link/PWA
serviceWorkerRegistration.unregister();

// Якщо ви хочете почати вимірювати продуктивність у вашій програмі, передайте функцію
// для логування результатів (наприклад: reportWebVitals(console.log))
// або відправки до аналітичного endpoint. Дізнайтеся більше: https://bit.ly/CRA-vitals
reportWebVitals();
