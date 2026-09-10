document.addEventListener("DOMContentLoaded", async () => {
    try {
        const res = await fetch("/api/protocol");
        const data = await res.json();

        // 1. Tampilkan Protocol Version
        document.getElementById("protocol-version").textContent = `Protocol ${data.protocolVersion}`;

        // 2. Tampilkan Daftar Event Types yang didukung
        const eventsContainer = document.getElementById("supported-events");
        eventsContainer.innerHTML = "";
        data.supportedEvents.forEach(evt => {
            const badge = document.createElement("span");
            badge.className = "badge";
            badge.textContent = evt;
            eventsContainer.appendChild(badge);
        });

        // 3. Tampilkan Sample Payload
        document.getElementById("sample-payload").textContent = JSON.stringify(data.samplePayload, null, 2);
    } catch (err) {
        console.error("Gagal memuat diagnostic protocol:", err);
        document.getElementById("protocol-version").textContent = "Gagal terhubung ke API";
    }
});