$(".show-hide-password-login").on("click", e => {

    const senha = $(".input-login-senha")
    const btn_show = $(".show-password")
    const btn_hide = $(".hide-password")

    if (senha.attr("type") == "password") {
        btn_show.css("display", "none")
        btn_hide.css("display", "block")
        senha.attr("type", "text")
    } else {
        btn_show.css("display", "block")
        btn_hide.css("display", "none")
        senha.attr("type", "password")
    }
})