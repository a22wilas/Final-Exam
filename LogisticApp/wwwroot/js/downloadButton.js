
function downloadAPICSV() {
    const link = document.createElement("a");
    link.setAttribute("href", "?handler=DownloadCsv");
    link.setAttribute("download", "API_logistic_results.csv");
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

function downloadMQCSV() {
    const link = document.createElement("a");
    link.setAttribute("href", "?handler=DownloadCsv");
    link.setAttribute("download", "MQ_logistic_results.csv");
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}