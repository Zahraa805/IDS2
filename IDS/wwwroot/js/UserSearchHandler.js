document.getElementById('searchBox').addEventListener('input', function () {

   
    let controllerName = document.getElementById('controllerName').value;
    let keyword = this.value.trim(); // Remove spaces
    let baseUrl = document.getElementById('baseUrl').value;


   // alert(`Badr + ${baseUrl} + badr`);

    fetch(`${baseUrl}${controllerName}/Search?keyword=${encodeURIComponent(keyword)}`)
        .then(response => response.text())
        .then(data => {
            let tableBody = document.getElementById('entityTableBody');

            if (keyword === '') {
                // If input is empty, reload all entities
                fetch(`${baseUrl}${controllerName}/Search?keyword=`)
                    .then(response => response.text())
                    .then(data => {
                        tableBody.innerHTML = data;
                    });
            } else {
                tableBody.innerHTML = data.trim() ? data : '<tr><td colspan="2" class="text-center">لم يتم العثور على نتائج.</td></tr>';
            }
        });

});