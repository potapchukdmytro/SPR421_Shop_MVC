// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function deleteCategory(data) {
    const category = JSON.parse(data);
    const message = document.getElementById("categoryDeleteMessage");
    const link = document.getElementById("categoryDeleteUrl");
    message.innerText = `Ви дійсно хочете видалити категорію "${category.Name}"?`;
    link.href = "/Category/Delete/" + category.Id;
}

document.addEventListener("DOMContentLoaded", function () {
    const total = document.getElementById("total");
    const subtotal = document.getElementById("subtotal");
    total.innerText = subtotal.innerText;
});