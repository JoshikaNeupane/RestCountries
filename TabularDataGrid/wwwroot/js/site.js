// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function showLoading(elemId) {

    $("#" + elemId).html("<div class='text-center m-5 bg-white'><i class='fa fa-4x fa-spinner fa-pulse' aria-hidden='true'> </i>Loading..</div>");
}