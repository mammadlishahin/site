function navin_icindeki_buttonlari_duzenle() {
    try {
        const Terapilerim = '<li class="nav-item"><a class="nav-link button-Terapilerim" href="/Work/Terapiler">Terapilerim</a></li>'
        const Profil = '<li class="nav-item"><a class="nav-link button-Profil" href="/Work/Profil">Profil</a></li>'


        document.querySelector("nav .link-list").innerHTML = Terapilerim
        document.querySelector("nav .link-list").insertAdjacentHTML("beforeend", Profil)
        

    } catch (error) {
        console.log('button-RandevuOlustur siline bilmir')
        console.error(error)
    }

}
navin_icindeki_buttonlari_duzenle();

