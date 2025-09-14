document.addEventListener("DOMContentLoaded", function () {
    const countInput = document.getElementById("countInput");
    const bookIntPrice = document.getElementById("bookIntPrice");
    const bookShowPrice = document.getElementById("bookShowPrice");

    const incrementBtn = document.querySelector("button.increment");
    const decrementBtn = document.querySelector("button.decrement");

    if (!countInput || !bookIntPrice || !bookShowPrice) return;

    function updatePrice() {
        let count = parseInt(countInput.value) || 1;

        if (count < 1) count = 1;
        if (count > 20) count = 20;

        countInput.value = count;

        const totalPrice = count * parseInt(bookIntPrice.value);
        bookShowPrice.textContent = totalPrice.toLocaleString("fa-IR") + " تومان";
    }

    // وقتی کاربر مستقیم عدد را تغییر داد
    countInput.addEventListener("input", updatePrice);

    // دکمه +
    incrementBtn.addEventListener("click", () => {
        countInput.value = parseInt(countInput.value || 0) + 1;
        updatePrice();
    });

    // دکمه -
    decrementBtn.addEventListener("click", () => {
        countInput.value = parseInt(countInput.value || 1) - 1;
        updatePrice();
    });

    // مقدار اولیه
    updatePrice();
});

//document.addEventListener("DOMContentLoaded", function () {
//    const countInput = document.getElementById("countInput");
//    const bookIntPrice = document.getElementById("bookIntPrice");
//    const bookShowPrice = document.getElementById("bookShowPrice");
//    const incrementBtn = document.querySelector(".increment");
//    const decrementBtn = document.querySelector(".decrement");

//    if (!countInput || !bookIntPrice || !bookShowPrice) return;

//    // تابع بروزرسانی قیمت
//    function updatePrice() {
//        let count = parseInt(countInput.value) || 1;

//        if (count < 1) count = 1;
//        if (count > 20) count = 20;

//        countInput.value = count;

//        const totalPrice = count * parseInt(bookIntPrice.value);
//        bookShowPrice.textContent = totalPrice.toLocaleString("fa-IR") + " تومان";
//    }

//    // رویداد تغییر input
//    countInput.addEventListener("change", updatePrice);

//    // دکمه +
//    incrementBtn?.addEventListener("click", () => {
//        countInput.value = parseInt(countInput.value || 0) + 1;
//        updatePrice();
//    });

//    // دکمه -
//    decrementBtn?.addEventListener("click", () => {
//        countInput.value = parseInt(countInput.value || 1) - 1;
//        updatePrice();
//    });

//    // مقدار اولیه
//    updatePrice();
//});

// COLOR SELCET
const colorButtons = document.querySelectorAll(".color-select-btn");
const colorTitle = document.querySelector(".color-title");

colorButtons?.forEach((button) => {
    button.addEventListener("click", () => {
        colorButtons.forEach((btn) => {
            btn.classList.remove("ring-4", "ring-blue-400");
            btn.classList.add("ring-1", "ring-gray-400");
        });

        button.classList.remove("ring-1", "ring-gray-400");
        button.classList.add("ring-4", "ring-blue-400");

        const span = button.querySelector("span");
        const classList = span.classList;
        const colorClass = Array.from(classList).find(c => c.startsWith("bg-"));

        const colorMap = {
            "bg-black": "مشکی",
            "bg-white": "سفید",
            "bg-green-400": "سبز",
            "bg-blue-500": "آبی"
        };

        const colorName = colorMap[colorClass] || "نامشخص";
        colorTitle.textContent = `رنگ : ${colorName}`;
    });
});



// TEXT SLIDER 
document.addEventListener("DOMContentLoaded", () => {
    const texts = [
        { text: "🔥 ۱۰۰۰+ فروش در هفته گذشته", color: "text-red-500" },
        { text: "💯 ۵۰۰+ نفر بیش از ۲ بار این کالا را خریده‌اند", color: "text-green-600" },
        { text: "🛒 در سبد خرید ۱۰۰۰+ نفر", color: "text-blue-600" }
    ];

    let index = 0;
    const slider = document.getElementById("slider-text");

    setInterval(() => {
        index = (index + 1) % texts.length;
        slider.classList.add("opacity-0");

        setTimeout(() => {
            slider.innerHTML = `<p class="${texts[index].color}">${texts[index].text}</p>`;
            slider.classList.remove("opacity-0");
        }, 300);
    }, 3000);
});



// CHANGE TAB 
document.addEventListener("DOMContentLoaded", () => {
    const buttonsTab = document.querySelectorAll(".tab-btn");
    const contents = document.querySelectorAll(".tab-content");

    buttonsTab.forEach((btn) => {
        btn.addEventListener("click", () => {
            const target = btn.getAttribute("data-target");

            buttonsTab.forEach((b) => {
                b.classList.remove("text-blue-500");
                b.classList.add("text-gray-500", "dark:text-gray-300");
            });
            btn.classList.remove("text-gray-500", "dark:text-gray-300");
            btn.classList.add("text-blue-500");

            contents.forEach((content) => {
                if (content.classList.contains(target)) {
                    content.classList.remove("hidden");
                    content.classList.add("block");
                } else {
                    content.classList.remove("block");
                    content.classList.add("hidden");
                }
            });
        });
    });
});




// SHOW MORE COMMENTS
const moreCommentBtn = document.querySelector('.more-comment-btn');
const moreCommentText = document.querySelector('.more-comment-text');
const moreCommentIcon = document.querySelector('.more-comment-icon');
const hiddenCommentItems = document.querySelectorAll('.hidden-comment-item');

if (moreCommentBtn) {
    moreCommentBtn.addEventListener('click', () => {
        hiddenCommentItems.forEach(item => {
            item.classList.toggle('hidden');
            item.classList.toggle('block');
        });

        if (moreCommentText.innerHTML === 'مشاهده بیشتر') {
            moreCommentText.innerHTML = 'مشاهده کمتر';
        } else {
            moreCommentText.innerHTML = 'مشاهده بیشتر';
        }

        moreCommentIcon.classList.toggle('rotate-180');
    });
}




// PRODUCT SLIDER

const openSliderModals = document.querySelectorAll('.open-sliderModal')
const sliderModal = document.querySelector('.slider-modal')
const overlayProductPage = document.querySelector('.overlay')
const closeSliderModal = document.querySelector('.close-sliderModal')

openSliderModals.forEach(el => {
    el.addEventListener('click', () => {
        sliderModal.classList.add('active')
        overlayProductPage.classList.add('active')
    })
})

overlayProductPage.addEventListener('click', () => {
    overlayProductPage.classList.remove('active')
    sliderModal.classList.remove('active')
})

closeSliderModal.addEventListener('click', () => {
    sliderModal.classList.remove('active')
    overlayProductPage.classList.remove('active')
})





