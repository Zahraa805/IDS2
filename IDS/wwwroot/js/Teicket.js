// Toggle menu function
toggleMenu = () => {
    document.querySelector('.nav-links').classList.toggle('active');
};

// Validate phone number (Egyptian format)
validatePhone = (input) => {
    const phonePattern = /^01[0-9]{9}$/;
    if (!phonePattern.test(input.value)) {
        alert('رقم الهاتف غير صحيح! يجب أن يكون 11 رقمًا ويبدأ بـ 01');
        input.value = '';
    }
};

// Validate national ID and auto-fill age
calculateAge = (input) => {
    const idPattern = /^[0-9]{14}$/;
    if (!idPattern.test(input.value)) {
        alert('الرقم القومي يجب أن يكون 14 رقمًا');
        input.value = '';
        return;
    }

    let birthYear = (input.value[0] == '2' ? '19' : '20') + input.value.substr(1, 2);
    let birthMonth = input.value.substr(3, 2);
    let birthDay = input.value.substr(5, 2);

    let birthDate = new Date(birthYear, birthMonth - 1, birthDay);
    let today = new Date();
    let age = today.getFullYear() - birthDate.getFullYear();

    if (today < new Date(today.getFullYear(), birthDate.getMonth(), birthDate.getDate())) {
        age--;
    }

    document.querySelector('input[name="السن"]').value = age;
};

// Auto-fill gender based on national ID
detectGender = (input) => {
    let genderDigit = input.value[12];
    document.querySelector('input[name="النوع"]').value = genderDigit % 2 === 0 ? 'أنثى' : 'ذكر';
};

// Restrict number fields to only allow numeric input
restrictToNumbers = (input) => {
    input.value = input.value.replace(/[^0-9]/g, '');
};

// Check if at least one general checkbox is selected
validateCheckboxes = () => {
    let checkboxes = document.querySelectorAll('input[type="checkbox"]:not(.medical-history)');
    let checked = Array.from(checkboxes).some(checkbox => checkbox.checked);

    if (!checked) {
        alert('يجب اختيار عيادة واحدة على الأقل!');
        return false;
    }
    return true;
};

// Validate form before submission
validateForm = (event) => {
    let requiredFields = document.querySelectorAll('input[type="text"], input[type="date"], textarea');

    for (let field of requiredFields) {
        if (field.value.trim() === '') {
            alert(`يرجى ملء جميع الحقول المطلوبة! (${field.name})`);
            field.focus();
            event.preventDefault();
            return false;
        }
    }

    if (!validateCheckboxes()) {
        event.preventDefault();
        return false;
    }

    alert('تم إرسال البيانات بنجاح!');

    // ⏳ تأخير بسيط لعرض التنبيه قبل الانتقال
    setTimeout(() => {
        goToNextPage();
    }, 1000);
};

// Navigate to the next page
goToNextPage = () => {
    window.location.href = 'teeth.html'; // غيري الرابط حسب اسم الصفحة المطلوبة
};

// Attach events
document.addEventListener('DOMContentLoaded', () => {
    document.querySelector('input[name="رقم الهاتف"]').addEventListener('blur', function () {
        validatePhone(this);
    });

    document.querySelector('input[name="الرقم القومي"]').addEventListener('blur', function () {
        calculateAge(this);
        detectGender(this);
    });

    document.querySelectorAll('input[type="text"]').forEach(input => {
        if (input.name.includes('رقم') || input.name.includes('السن')) {
            input.addEventListener('input', function () {
                restrictToNumbers(this);
            });
        }
    });

    // ربط زر الإرسال بالتحقق من الفورم
    document.querySelector('.submit-btn').addEventListener('click', validateForm);
});
