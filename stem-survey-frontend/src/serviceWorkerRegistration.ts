// Цей опціональний код використовується для реєстрації service worker.
// register() не викликається за замовчуванням.

// Це дозволяє програмі завантажуватися швидше при наступних відвідуваннях у production, і надає
// можливості роботи офлайн. Однак, це також означає, що розробники (та користувачі)
// побачать розгорнуті оновлення тільки при наступних відвідуваннях сторінки, після того як всі
// існуючі вкладки, відкриті на сторінці, будуть закриті, оскільки раніше кешовані
// ресурси оновлюються у фоновому режимі.

// Щоб дізнатися більше про переваги цієї моделі та інструкції щодо того, як
// підключитися, прочитайте https://cra.link/PWA

const isLocalhost = Boolean(
  window.location.hostname === 'localhost' ||
    // [::1] це IPv6 localhost адреса.
    window.location.hostname === '[::1]' ||
    // 127.0.0.0/8 вважаються localhost для IPv4.
    window.location.hostname.match(/^127(?:\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}$/)
);

type Config = {
  onSuccess?: (registration: ServiceWorkerRegistration) => void;
  onUpdate?: (registration: ServiceWorkerRegistration) => void;
};

export function register(config?: Config) {
  if (process.env.NODE_ENV === 'production' && 'serviceWorker' in navigator) {
    // URL конструктор доступний у всіх браузерах, що підтримують SW.
    const publicUrl = new URL(process.env.PUBLIC_URL, window.location.href);
    if (publicUrl.origin !== window.location.origin) {
      // Наш service worker не працюватиме, якщо PUBLIC_URL знаходиться на іншому origin
      // ніж та сторінка, на якій ми обслуговуємося. Це може статися, якщо CDN використовується для
      // обслуговування ресурсів; див. https://github.com/facebook/create-react-app/issues/2374
      return;
    }

    window.addEventListener('load', () => {
      const swUrl = `${process.env.PUBLIC_URL}/service-worker.js`;

      if (isLocalhost) {
        // Це запущено на localhost. Давайте перевіримо, чи існує service worker ще чи ні.
        checkValidServiceWorker(swUrl, config);

        // Додамо додаткове логування для localhost, вказуючи розробникам на
        // документацію service worker/PWA.
        navigator.serviceWorker.ready.then(() => {
          console.log(
            'Ця веб-програма обслуговується cache-first service ' +
              'worker. Щоб дізнатися більше, відвідайте https://cra.link/PWA'
          );
        });
      } else {
        // Не localhost. Просто реєструємо service worker
        registerValidSW(swUrl, config);
      }
    });
  }
}

function registerValidSW(swUrl: string, config?: Config) {
  navigator.serviceWorker
    .register(swUrl)
    .then((registration) => {
      registration.onupdatefound = () => {
        const installingWorker = registration.installing;
        if (installingWorker == null) {
          return;
        }
        installingWorker.onstatechange = () => {
          if (installingWorker.state === 'installed') {
            if (navigator.serviceWorker.controller) {
              // На цьому етапі оновлений попередньо кешований контент був отриманий,
              // але попередній service worker все ще буде обслуговувати старіший
              // контент, поки всі клієнтські вкладки не будуть закриті.
              console.log(
                'Новий контент доступний і буде використовуватися, коли всі ' +
                  'вкладки для цієї сторінки будуть закриті. Див. https://cra.link/PWA.'
              );

              // Виконуємо callback
              if (config && config.onUpdate) {
                config.onUpdate(registration);
              }
            } else {
              // На цьому етапі все було попередньо кешовано.
              // Це ідеальний час для відображення
              // повідомлення "Контент кешовано для офлайн використання."
              console.log('Контент кешовано для офлайн використання.');

              // Виконуємо callback
              if (config && config.onSuccess) {
                config.onSuccess(registration);
              }
            }
          }
        };
      };
    })
    .catch((error) => {
      console.error('Помилка під час реєстрації service worker:', error);
    });
}

function checkValidServiceWorker(swUrl: string, config?: Config) {
  // Перевіряємо, чи можна знайти service worker. Якщо ні - перезавантажуємо сторінку.
  fetch(swUrl, {
    headers: { 'Service-Worker': 'script' },
  })
    .then((response) => {
      // Переконуємося, що service worker існує, і що ми дійсно отримуємо JS файл.
      const contentType = response.headers.get('content-type');
      if (
        response.status === 404 ||
        (contentType != null && contentType.indexOf('javascript') === -1)
      ) {
        // Service worker не знайдено. Ймовірно, це інша програма. Перезавантажуємо сторінку.
        navigator.serviceWorker.ready.then((registration) => {
          registration.unregister().then(() => {
            window.location.reload();
          });
        });
      } else {
        // Service worker знайдено. Продовжуємо як зазвичай.
        registerValidSW(swUrl, config);
      }
    })
    .catch(() => {
      console.log('Інтернет-з\'єднання не знайдено. Програма працює в офлайн режимі.');
    });
}

export function unregister() {
  if ('serviceWorker' in navigator) {
    navigator.serviceWorker.ready
      .then((registration) => {
        registration.unregister();
      })
      .catch((error) => {
        console.error(error.message);
      });
  }
}
