
document.querySelector(".kayitOl label").addEventListener('click', function () {
    document.querySelectorAll(".girisYap input").forEach(function (input) {
        if (input.type == "hidden") { } else {
            input.value = "";
        }
    });
});

document.querySelector(".girisYap label").addEventListener('click', function () {
    document.querySelectorAll(".kayitOl input").forEach(function (input) {
        if (input.type == "hidden") { } else {
            input.value = "";
        }
    });
});


// Yalnızca bir kez çalışacak fonksiyon
function sadeceBirKereCalis() {
    // Fonksiyonun yalnızca bir kez çalıştığını kontrol etmek için bir değişken kullanabiliriz
    const PostType = document.getElementById("ViewBag_PostType").value;
    if (!sadeceBirKereCalis.calisti && PostType === '1') {
        const kayitBTN=document.getElementById('kayitOlButton');
        kayitBTN.click()
        kayitBTN.click()
        kayitBTN.click()
        kayitBTN.click()

        document.querySelectorAll(".kayitOl span").forEach(function (span) {
            // span.classList.remove("d-block");
            // span.classList.add("d-none");
        });
        sadeceBirKereCalis.calisti = true;
    } else if (!sadeceBirKereCalis.calisti && PostType === '2') {
        const girisBTN=document.getElementById('girisYapButton');
        girisBTN.click();

        document.querySelectorAll(".girisYap span").forEach(function (span) {
            // span.classList.remove("d-block");
            // span.classList.add("d-none");
        });
        sadeceBirKereCalis.calisti = true;
    }
}
sadeceBirKereCalis();



var inputs = document.querySelectorAll('.kayitOl input:is([type="password"])');
var sayac = [0, 0];
inputs[0].addEventListener('focus', function () {
    sayac[0] = 1;
});
inputs[1].addEventListener('focus', function () {
    sayac[1] = 1;
});
var errorTagSpan = document.querySelector('.kayitOl #SifrelerEslesmiyor');
// Her bir input alanı için döngü
inputs.forEach(function (inputElement) {
    inputs = document.querySelectorAll('.kayitOl input:is([type="password"])');
    inputElement.addEventListener('focus', function () {
        errorTagSpan.classList.add('d-none');
    });

    inputElement.addEventListener('blur', function () {

        if (sayac[0] === sayac[1] && sayac[0] > 0 && inputs[0].value.length > 7 && inputs[0].value != inputs[1].value) {
            errorTagSpan.classList.remove('d-none');
            errorTagSpan.classList.add('d-block');
        }
    });
});



// Şifreler Eşleşmiyor Anonim Metodu.
// İşleyişi Şöyle Kayıt olurken iki Password alanı Farklıylsa Form gönderilmesini engelle ve 'Şifreler Eşleşmiyor' Hatası Yanıp-Söndür.
document.querySelector(".kayitOl form").addEventListener("submit", async function (event) {

    inputs = document.querySelectorAll('.kayitOl input:is([type="password"])');
    if (inputs[0].value != inputs[1].value) {
        // Form gönderilmesini engelle.
        event.preventDefault();
        // Bir döngü oluştur
        // 'Şifreler Eşleşmiyor' Hatası Yanıp-Sönsün.
        for (let i = 1; i < 9; i++) {
            if (i % 2 == 1) {
                errorTagSpan.classList.remove('text-danger');
                errorTagSpan.classList.add('text-white');

            } else {
                errorTagSpan.classList.remove('text-white');
                errorTagSpan.classList.add('text-danger');
            }
            // Hər 0.1 saniyədə bir Yanıp-Sönsün.
            await new Promise(resolve => setTimeout(resolve, 100));

        } // for

    } // if

}); // Şifreler Eşleşmiyor Anonim Metodu.



document.addEventListener("DOMContentLoaded", function () {
    var toggleButtons = document.querySelectorAll(".toggle-password");
    toggleButtons.forEach(function (button) {
        button.addEventListener("click", function () {
            var inputId = this.getAttribute("toggle");
            var input = document.getElementById(inputId);
            if (input.type === "password") {
                input.type = "text";
            } else {
                input.type = "password";
            }
            this.classList.toggle("fa-eye");
            this.classList.toggle("fa-eye-slash");
        });
    });
});
