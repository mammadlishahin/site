

function navin_altina_dikkat_adimn_sayfasindasiniz_yaz() {
    try {
        const bildiri_divi = '<div class="mt-3 alert-danger h6 py-1 m-0 w-100 text-center">Dikkat: Şu an Admin Yetkisine Sahipsiniz.</div>'
        document.querySelector("nav").insertAdjacentHTML("beforeend", bildiri_divi)
    } catch (error) {
        console.log('error')
        console.log(error)
    }
}
navin_altina_dikkat_adimn_sayfasindasiniz_yaz();



function navin_icindeki_buttonlari_duzenle() {
    try {
        const Seanslar = '<li class="nav-item"> <a class="nav-link button-Terapilerim" href="/Admin/Seanslar" >Seanslar</a> </li>'
        const SorulanSorular = '<li class="nav-item"> <a class="nav-link button-Terapilerim" href="/Admin/SorulanSorular" >Sorular</a> </li>'
        const Panel = '<li class="nav-item"> <a class="nav-link button-Terapilerim" href="/Admin/Panel" >Panel</a> </li>'
        document.querySelector("nav").style.paddingBottom="0px"
        document.querySelector("nav .link-list").innerHTML=Seanslar
        document.querySelector("nav .link-list").insertAdjacentHTML("beforeend", Panel)
        document.querySelector("nav .link-list").insertAdjacentHTML("beforeend", SorulanSorular)
        // Index sayfasındakı ortadakı büyük Randevu Oluştur buttonunu sil.
        var randeuOlusturBTN=document.querySelector(".button-RandevuOlustur");
        if (randeuOlusturBTN!=null) {
            randeuOlusturBTN.remove();
        }
        
    } catch (error) {
        console.log('button-RandevuOlustur siline bilmir')
        console.error(error)
    }

    try {
        // Social Media iconlarını sil.
        document.querySelector(".social").remove();
    } catch (error) {
        console.log('social siline bilmir')
        console.error(error)
    }
}
navin_icindeki_buttonlari_duzenle();