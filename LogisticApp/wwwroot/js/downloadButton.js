
function downloadCSV() {
    const table = document.getElementById("dataTable");
    const rows = table.querySelectorAll("tr");
    let csv = [];

    rows.forEach(row => {
        const cols = row.querySelectorAll("th, td");
        const rowData = Array.from(cols).map(col => {
            // wrap in quotes to handle commas in data
            return `"${col.innerText.replace(/"/g, '""')}"`;
        });
        csv.push(rowData.join(","));
    });

    const csvContent = csv.join("\n");
    const blob = new Blob([csvContent], { type: "text/csv;charset=utf-8;" });
    const url = URL.createObjectURL(blob);

    const link = document.createElement("a");
    link.setAttribute("href", url);
    link.setAttribute("download", "logistic_results.csv");
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}
