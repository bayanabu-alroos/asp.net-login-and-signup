// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.querySelector('form').addEventListener('submit', function (e) {
    if (!confirm('Are you sure you want to delete the selected users?')) {
        e.preventDefault();
    }
});