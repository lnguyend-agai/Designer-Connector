const socialLinks = document.getElementsByClassName('social-link');
for (i = 0; i < socialLinks.length; i++) {
    let socialLink = socialLinks[i];
    socialLink.addEventListener('click', function (e) {
        e.preventDefault();
        window.chrome.webview.postMessage(this.getAttribute('href'));
    });
}

const partners = document.getElementsByClassName('col-xs-3 partner');
for (i = 0; i < partners.length; i++) {
    let partner = partners[i];
    partner.addEventListener('click', function (e) {
        e.preventDefault();
        window.chrome.webview.postMessage(partner.children[0].href);
    });
}

function onBodyClick_PDFfile(ev) {
    a = ev.target.href;
    if (ev.target.tagName === "A" && (ev.target.rel === "noopener") && a.startsWith("https://www.mtextur.com/materials/")) {
        ev.preventDefault();
        ev.stopPropagation();
        window.chrome.webview.postMessage(ev.target.href);
    }
}
document.body.addEventListener("click", onBodyClick_PDFfile, true);

function onBodyClick_DownloadPrevent(ev) {
    if (ev.target.tagName === "BUTTON" && (ev.target.id === "download-option-zip" || ev.target.id === "download-option-usdz" || ev.target.id ==="download-option-glb")) {
        alert("The type of the downloaded file isn't supported. Please try downloading an SKP file.");
        ev.preventDefault();
        ev.stopPropagation();
    }
}
document.body.addEventListener("click", onBodyClick_DownloadPrevent, true);

function onBodyClick_HiddenIcon(ev) {
	if (ev.target.tagName === "I" && (ev.target.className === "fa fa-facebook" || ev.target.className === "fa fa-linkedin" || ev.target.className === "fa fa-twitter" || ev.target.className === "fa fa-pinterest")){
        ev.preventDefault();
        ev.stopPropagation();
		window.chrome.webview.postMessage(ev.target.parentNode.href);
	}
}
document.body.addEventListener("click", onBodyClick_HiddenIcon, true);

function onClickButton(ev) {
    if (ev.target.className == "btn-lightup btn-buy-sketchup" || ev.target.id == "try-sketchup-button") {
        ev.preventDefault();
        ev.stopPropagation();
        window.chrome.webview.postMessage("https://www.sketchup.com/try-sketchup");
    }
    else if (ev.target.id == "manage-profile-link") {
        ev.preventDefault();
        ev.stopPropagation();
        window.chrome.webview.postMessage(ev.target.href);
    }
    else if (ev.target.href == "https://extensions.sketchup.com/" || ev.target.href == "https://www.sketchup.com/") {
        ev.preventDefault();
        ev.stopPropagation();
        window.chrome.webview.postMessage(ev.target.href);
    }
    else if (ev.target.href.startsWith("https://www.trimble.com")) {
        ev.preventDefault();
        ev.stopPropagation();
        window.chrome.webview.postMessage(ev.target.href);
    }
}
document.body.addEventListener("click", onClickButton, true);