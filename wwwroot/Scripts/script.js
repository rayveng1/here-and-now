function GetPOI(){
    let searchModal = document.getElementById("SearchModal");
    console.log(`HI!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!:${searchModal.nextElementSibling.value}`)
    searchModal.nextElementSibling.outerHTML = `
    <div class="modal-content">
        <div class="modal-header">
            <h1 class="modal-title fs-5" id="POIResult">What are you in the mood for?</h1>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <h2 class="fs-5 m-3">Filters</h2>
        <div class="modal-body d-flex flex-column pt-0">
            
            <label for="radius" class="form-label fw-bold fs-5">Search Radius</label>
            <div>
                <input style="color:#00CC99" type="range" class="form-range" min="1" max="31" step="5" id="radius"
                    oninput="this.nextElementSibling.value = this.value">
                <output class="d-inline-flex fw-light fst-italic">16</output>
                <p class="d-inline-flex fw-light fst-italic" style="margin: 0;"> miles</p>
            </div>
        </div>
        <div class="modal-footer">
            <button type="button" class="btn btn-primary w-100" style="background-color: var(--primary-color); border: none" onclick="GetPOI()">Search</button>
        </div>
    </div>`;
}

$(document).ready(function () {
    $('#GemModal').modal('show');
});