// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//var baris2 = document.getElementById("baris2");
//var semula = baris2.innerHTML;
//var btn = document.getElementById("btn");
//var btn2 = document.getElementById("btn2");

//btn.addEventListener("click", function () {
//    baris2.style.backgroundColor = "lightblue";
//});

//baris2.addEventListener("mouseenter", function () {
//    baris2.innerHTML = "Kursors didalam";
//    baris2.style.backgroundColor = "red";
//});

//baris2.addEventListener("mouseleave", function () {
//    baris2.innerHTML = semula;
//    baris2.style.backgroundColor = "white";
//});

const animals = [
    { name: "Fluffy", species: "cat", class: { name: "mamalia" } },
    { name: "Nemo", species: "fish", class: { name: "invertebrata" } },
    { name: "Garfield", species: "cat", class: { name: "mamalia" } },
    { name: "Dory", species: "fish", class: { name: "invertebrata" } },
    { name: "Camello", species: "cat", class: { name: "mamalia" } }
]

const onlyCat = [];
let i = 0;
while (i < animals.length) {
    if (animals[i].species == "cat") {
        onlyCat.push(animals[i]);
    }
    if (animals[i].species == "fish") {
        animals[i].class.name = "Non-Mamalia";
    }
    i++;
}

console.log(animals);
console.log(onlyCat);