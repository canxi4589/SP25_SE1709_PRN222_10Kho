// wwwroot/js/chart.js
let heightChart = null;
let weightChart = null;

window.initializeCharts = (heightCanvasId, weightCanvasId, labels) => {
    // Destroy existing charts if they exist
    if (heightChart) {
        heightChart.destroy();
    }
    if (weightChart) {
        weightChart.destroy();
    }

    // Initialize Height Chart
    const heightCtx = document.getElementById(heightCanvasId).getContext('2d');
    heightChart = new Chart(heightCtx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: []
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                x: {
                    title: {
                        display: true,
                        text: 'Age (Years)'
                    }
                },
                y: {
                    title: {
                        display: true,
                        text: 'Height (cm)'
                    },
                    beginAtZero: false
                }
            },
            elements: {
                line: {
                    tension: 0.4 // Smooth lines (similar to NaturalSpline in MudChart)
                }
            }
        }
    });

    // Initialize Weight Chart
    const weightCtx = document.getElementById(weightCanvasId).getContext('2d');
    weightChart = new Chart(weightCtx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: []
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                x: {
                    title: {
                        display: true,
                        text: 'Age (Years)'
                    }
                },
                y: {
                    title: {
                        display: true,
                        text: 'Weight (kg)'
                    },
                    beginAtZero: false
                }
            },
            elements: {
                line: {
                    tension: 0.4 // Smooth lines
                }
            }
        }
    });
};

window.updateHeightChart = (standardData, overData, underData, userData) => {
    if (!heightChart) return;

    heightChart.data.datasets = [
        {
            label: 'Standard Height',
            data: standardData,
            borderColor: 'blue',
            fill: false,
            pointRadius: 0 // No points on the line
        },
        {
            label: 'Over Height',
            data: overData,
            borderColor: 'green',
            fill: false,
            pointRadius: 0
        },
        {
            label: 'Under Height',
            data: underData,
            borderColor: 'red',
            fill: false,
            pointRadius: 0
        },
        {
            label: 'Your Height',
            data: userData,
            borderColor: 'transparent', // Invisible line
            pointRadius: userData.map(value => (isNaN(value) ? 0 : 6)), // Show point only at user data
            pointBackgroundColor: '#ff9800', // Orange marker
            pointBorderColor: '#ff9800',
            showLine: false // Disable line drawing
        }
    ];
    heightChart.update();
};

window.updateWeightChart = (standardData, overData, underData, userData) => {
    if (!weightChart) return;

    weightChart.data.datasets = [
        {
            label: 'Standard Weight',
            data: standardData,
            borderColor: 'blue',
            fill: false,
            pointRadius: 0
        },
        {
            label: 'Over Weight',
            data: overData,
            borderColor: 'green',
            fill: false,
            pointRadius: 0
        },
        {
            label: 'Under Weight',
            data: underData,
            borderColor: 'red',
            fill: false,
            pointRadius: 0
        },
        {
            label: 'Your Weight',
            data: userData,
            borderColor: 'transparent',
            pointRadius: userData.map(value => (isNaN(value) ? 0 : 6)),
            pointBackgroundColor: '#ff9800',
            pointBorderColor: '#ff9800',
            showLine: false
        }
    ];
    weightChart.update();
};