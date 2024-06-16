window.addEventListener('DOMContentLoaded',(event) => {
    getVisitCount();
})

const functionApi = 'https://getresumecounteranuj.azurewebsites.net/api/GetResumeCounter?code=e8zrV_jgoHcvL6kNUEWu-eLonQM57ztV84A0zXEFznA8AzFuX9T3Mg%3D%3D';

const getVisitCount = () => {
    let count =30;
    fetch(functionApi).then(response => {
        return response.json()
    }).then(response =>{
        console.log("Website Called Function API");
        count = response.count;
        document.getElementById("counter").innerText = count;
    }).catch(function(error){
        console.log(error);
    });
    return count;
}
