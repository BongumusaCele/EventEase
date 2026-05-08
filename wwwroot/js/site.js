// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
window.EventEase = window.EventEase || {};

(function () {
    const addressInputs = document.querySelectorAll('[data-address-search]');

    if (!addressInputs.length) {
        return;
    }

    const debounce = (callback, delay) => {
        let timer;
        return (...args) => {
            window.clearTimeout(timer);
            timer = window.setTimeout(() => callback(...args), delay);
        };
    };

    const createSearchUi = (input) => {
        const wrapper = document.createElement('div');
        wrapper.className = 'address-search';

        input.parentNode.insertBefore(wrapper, input);
        wrapper.appendChild(input);

        const results = document.createElement('div');
        results.className = 'address-search-results';
        results.setAttribute('role', 'listbox');
        results.hidden = true;

        const status = document.createElement('div');
        status.className = 'address-search-status';
        status.hidden = true;

        wrapper.appendChild(results);
        wrapper.appendChild(status);

        return { wrapper, results, status };
    };

    const setStatus = (status, message) => {
        status.textContent = message || '';
        status.hidden = !message;
    };

    const hideResults = (results) => {
        results.innerHTML = '';
        results.hidden = true;
    };

    const formatAddress = (place) => {
        return place.display_name || '';
    };

    const searchAddresses = async (query, signal) => {
        const url = new URL('https://nominatim.openstreetmap.org/search');
        url.searchParams.set('format', 'jsonv2');
        url.searchParams.set('addressdetails', '1');
        url.searchParams.set('countrycodes', 'za');
        url.searchParams.set('limit', '6');
        url.searchParams.set('q', query);

        const response = await fetch(url.toString(), {
            headers: {
                Accept: 'application/json'
            },
            signal
        });

        if (!response.ok) {
            throw new Error('Address lookup failed.');
        }

        return response.json();
    };

    const renderResults = (input, results, places) => {
        results.innerHTML = '';

        if (!places.length) {
            const empty = document.createElement('div');
            empty.className = 'address-search-empty';
            empty.textContent = 'No South African addresses found. Try a more specific search.';
            results.appendChild(empty);
            results.hidden = false;
            return;
        }

        places.forEach((place) => {
            const address = formatAddress(place);
            const button = document.createElement('button');
            button.type = 'button';
            button.className = 'address-search-option';
            button.setAttribute('role', 'option');

            const icon = document.createElement('i');
            icon.className = 'fas fa-location-dot';

            const label = document.createElement('span');
            label.textContent = address;

            button.appendChild(icon);
            button.appendChild(label);

            button.addEventListener('click', () => {
                input.value = address;
                input.dispatchEvent(new Event('input', { bubbles: true }));
                input.dispatchEvent(new Event('change', { bubbles: true }));
                hideResults(results);
                input.focus();
            });

            results.appendChild(button);
        });

        results.hidden = false;
    };

    addressInputs.forEach((input) => {
        const { results, status } = createSearchUi(input);
        let controller;

        const runSearch = debounce(async () => {
            const query = input.value.trim();

            if (query.length < 3) {
                hideResults(results);
                setStatus(status, '');
                return;
            }

            if (controller) {
                controller.abort();
            }

            controller = new AbortController();
            setStatus(status, 'Searching South African addresses...');

            try {
                const places = await searchAddresses(query, controller.signal);
                renderResults(input, results, places);
                setStatus(status, '');
            } catch (error) {
                if (error.name === 'AbortError') {
                    return;
                }

                hideResults(results);
                setStatus(status, 'Address search is unavailable right now. You can still type the location manually.');
            }
        }, 350);

        input.addEventListener('input', runSearch);

        input.addEventListener('keydown', (event) => {
            if (event.key === 'Escape') {
                hideResults(results);
            }
        });

        document.addEventListener('click', (event) => {
            if (!input.closest('.address-search')?.contains(event.target)) {
                hideResults(results);
            }
        });
    });
})();
