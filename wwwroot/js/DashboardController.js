function showScreen(screenId) {
    const screens = document.querySelectorAll(".screen");

    screens.forEach(screen => screen.style.display = "none");

    const activeScreen = document.getElementById(screenId);
    activeScreen.style.display = "block";
}