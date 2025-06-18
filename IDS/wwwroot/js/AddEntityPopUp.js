document.getElementById('toggleButton').addEventListener('click', function () {
    var btnToOpen = document.getElementById('toggleButton');
    var myDiv = document.getElementById('container');
    if (myDiv.style.display === 'none') {
        myDiv.style.display = 'block'; // Show the div
        btnToOpen.innerHTML = "رجوع";

    } else {
        myDiv.style.display = 'none'; // Hide the div
        btnToOpen.innerHTML = "إضافه كليه جديده";

    }
});