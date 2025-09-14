//$(document).ready(function () {

//    const $input = $('.search-btn-open input[name="Search"]');
//    const $modal = $('.search-modal');
//    const $resultsList = $modal.find('ul').first(); // ul اول برای نتایج جستجو

//    $input.on('keyup', function () {
//        let query = $(this).val();
//        if (query.length < 2) {
//            $resultsList.empty();
//            $modal.hide();
//            return;
//        }

//        $.ajax({
//            url: '/Home/SearchHeader', // مسیر اکشن کنترلر
//            type: 'GET',
//            data: { term: query },
//            success: function (data) {
//                let html = '';
//                data.forEach(p => {
//                    html += `
//        <li>
//            <a href="/Product/ProductDetails/${p.id}" class="flex items-center gap-x-2">
//                <svg class="size-5"><use href="#search" /></svg>
//                ${p.name} - ${p.price} تومان
//            </a>
//            <svg class="size-4"><use href="#arrow-up-right" /></svg>
//        </li>
//    `;
//                });

//                if (data.length > 0) {
//                    $resultsList.html(html);
//                    $modal.show();
//                } else {
//                    $resultsList.empty();
//                    $modal.hide();
//                }
//            }
//        });
//    });

//    // کلیک خارج از modal -> مخفی کردن
//    $(document).click(function (e) {
//        if (!$(e.target).closest('.search-btn-open, .search-modal').length) {
//            $modal.hide();
//        }
//    });
//});

$(document).ready(function () {

    // ----- دسکتاپ -----
    const $desktopInput = $('.search-btn-open input[name="Search"]');
    const $desktopModal = $('.search-modal');
    const $desktopResultsList = $desktopModal.find('ul').first();

    // ----- موبایل -----
    const $mobileInput = $('.mobile_search-modal input[type="text"]');
    const $mobileModal = $('.mobile_search-modal');
    const $mobileResultsList = $mobileModal.find('ul').first();

    // تابع مشترک برای لود نتایج
    function loadResults(query, $resultsList, $modal) {
        if (query.length < 2) {
            $resultsList.empty();
            $modal.hide();
            return;
        }

        $.ajax({
            url: '/Home/SearchHeader', // مسیر اکشن جستجو
            type: 'GET',
            data: { term: query },
            success: function (data) {
                let html = '';
                data.forEach(p => {
                    html += `
                        <li>
                            <a href="/Product/ProductDetails/${p.id}" class="flex items-center gap-x-2">
                                <img src="/api/File/GetFile?filename=${encodeURIComponent(p.image)}" class="w-10 h-10 object-cover rounded"/>

                                <span>${p.name} - ${p.price} تومان</span>
                            </a>
                            <svg class="size-4"><use href="#arrow-up-right" /></svg>
                        </li>
                    `;
                });

                if (data.length > 0) {
                    $resultsList.html(html);
                    $modal.show();
                } else {
                    $resultsList.empty();
                    $modal.hide();
                }
            }
        });
    }

    // ----- دسکتاپ تایپ -----
    $desktopInput.on('keyup', function () {
        const query = $(this).val();
        loadResults(query, $desktopResultsList, $desktopModal);
    });

    // ----- موبایل تایپ -----
    $mobileInput.on('keyup', function () {
        const query = $(this).val();
        loadResults(query, $mobileResultsList, $mobileModal);
    });

    // کلیک خارج از دسکتاپ
    $(document).click(function (e) {
        if (!$(e.target).closest('.search-btn-open, .search-modal').length) {
            $desktopModal.hide();
        }
        if (!$(e.target).closest('.mobile_search-modal').length) {
            $mobileModal.hide();
        }
    });

});
