// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

(function (app) {

    app.appointments = function () {
        loadAppointments();
        loadAnnouncements();
        loadPatientsNotes();
        loadAssignedPatients();
        loadAppointmentsForReception();
        editAppointment();
    };
    app.appointment = function () {

        bookAppointment();
    };
    app.registerPatient = function () {
        registerPatient();
    };

    app.reception = function () {

        preparePatientsTable();
        searchPatientByPhoneOrName();
    };

    async function loadAppointments() {
        try {
            const response = await fetch('https://localhost:7117/api/Appointments', {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'

                }
            });

            if (!response.ok) {
                throw new Error('Failed to connect to API ');
            }

            const data = await response.json();

            const tableBody = document.querySelector('#appointment-table tbody');
            tableBody.innerHTML = '';

            data.forEach(appointment => {
                const row = document.createElement('tr');
                row.innerHTML = `
                    <td>${appointment.id}</td>
                    <td>${appointment.patientName}</td>
                    <td>${appointment.phone ?? 'N/A'}</td>
                    <td>${appointment.department}</td>
                    <td>${new Date(appointment.appointmentDate).toLocaleDateString()}</td>
                    <td>${appointment.status}</td>    
                `;
                tableBody.appendChild(row);
            });

        } catch (error) {

        }
    };
    async function loadAnnouncements() {
        try {
            const announcementResponse = await fetch('https://localhost:7117/api/Announcements', {
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }

            })
            if (!announcementResponse.ok) {
                throw new Error('Failed to fetch Announcements.')
            }
            const announcementData = await announcementResponse.json()
            const announcementSection = document.querySelector('.announcements ul');
            announcementSection.innerHTML = '';
            announcementData.forEach(announcement => {
                const listElement = document.createElement('li');
                listElement.innerHTML = `${announcement.id}: <strong>${announcement.title}</strong> ${announcement.content ?? 'No content'} (${new Date(announcement.createdAt).toLocaleDateString()})`;

                announcementSection.appendChild(listElement);
            })
        }
        catch (error) {
            console.error('Fetch error:', error);
            console.log(error);
        }
    };
    async function loadPatientsNotes() {
        try {
            const patientResponse = await fetch('https://localhost:7117/api/MedicalNotes',
                {
                    method: 'GET',
                    headers: {
                        'content-type': 'application/json'
                    }
                })
            if (!patientResponse.ok) {
                throw new Error('Failed to fetch Patient Notes');
            }
            const patientNotes = await patientResponse.json();
            // console.log('API response:', patientNotes);
            const notesTable = document.querySelector('#medical-notes-table tbody');

            notesTable.innerHTML = '';
            patientNotes.forEach(note => {
                const row = document.createElement('tr');
                row.innerHTML = `
                <td>${note.patientId}</td> 
                 <td>${note.ailment}</td>
                 <td>${note.temperature}°C</td>
                <td>${note.bmi}</td>`
                notesTable.appendChild(row);
            });

        }
        catch (error) {
            console.error('Fetch error:', error);
            console.log(error);

        }


    };
    async function loadAssignedPatients() {
        try {
            const response = await fetch('https://localhost:7117/api/Patients', {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'

                }
            });

            if (!response.ok) {
                throw new Error('Failed to connect to API ');
            }

            const data = await response.json();

            const tableBody = document.querySelector('#assigned-patient-table tbody');
            tableBody.innerHTML = '';

            data.forEach(patient => {
                const row = document.createElement('tr');
                row.innerHTML = `
                    <td>${patient.id}</td>
                    <td>${patient.name}</td>
                    <td>${patient.room}</td>
                    <td>${patient.gender}</td>
                    <td>${patient.status}</td>
                    
                `;
                tableBody.appendChild(row);
            });

        } catch (error) {

        }

    };
    async function loadAppointmentsForReception() {
        try {
            const response = await fetch('https://localhost:7117/api/Appointments', {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'

                }
            });

            if (!response.ok) {
                throw new Error('Failed to connect to API ');
            }

            const data = await response.json();

            const tableBody = document.querySelector('#patients-table tbody');
            tableBody.innerHTML = '';
            data.forEach(appointment => {
                /*const editBtn = `<a href="#" onclick="editAppointment(${appointment.id})">Edit</a>`;*/
                /*const editBtn = `<button class="btn btn-sm btn-primary editBtn" >Edit</button>`;*/
              
                const row = document.createElement('tr');
                row.innerHTML = `
                    <td class="id-cell">${appointment.id}</td>
                    <td>${appointment.patientName}</td>
                    <td>${appointment.phone ?? 'N/A'}</td>
                    <td>${appointment.department}</td>
                    <td>${new Date(appointment.appointmentDate).toLocaleDateString()}</td>
                    <td>${appointment.status}</td>  
                    <td><button class="btn btn-sm btn-primary editBtn">Edit</button></td>
                `;
                tableBody.appendChild(row);
               
            });
            
            
        } catch (error) {

        }

    };
/*    receptionist update appointment*/
    function editAppointment() {
        const tableBody = document.querySelector('#patients-table tbody');

        if (!tableBody) return;

        tableBody.addEventListener('click', function (e) {
            if (e.target && e.target.classList.contains('editBtn')) {
                const row = e.target.closest('tr');
                const idCell = row.querySelector('.id-cell');
                const id = idCell.textContent.trim();

                const data = { id: id, status: 'Attended to' };

                fetch(`https://localhost:7117/api/Appointments/${id}`, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(data)
                })
                    .then(response => {
                        if (!response.ok) {
                            return response.text().then(text => {
                                throw new Error(`API error ${response.status}: ${text}`);
                            });
                        }
                        return response.text();
                    })
                    .then(result => {
                        alert("Appointment updated: " + result);
                    })
                    .catch(error => {
                        console.error("Error updating appointment:", error);
                        alert("Error updating appointment: " + error.message);
                    });
            }
        });
    }



    function bookAppointment() {
        const appointmentForm = document.getElementById('patient-form');

        appointmentForm.onsubmit = submitAppointmentData;
    };
    function submitAppointmentData(e) {
        e.preventDefault();
        const appointmentForm = document.getElementById('patient-form');

        const name = appointmentForm.querySelector('#name');
        const date = appointmentForm.querySelector('#date');
        const speciality = appointmentForm.querySelector('#speciality');
        const booktype = appointmentForm.querySelector('#booking');
        const email = appointmentForm.querySelector('#email');




        const data = {
            patientName: name.value,
            appointmentDate: date.value,
            department: speciality.value,
            status: booktype.value,
            email: email.value
        }

        try {
            fetch('https://localhost:7117/api/Appointments', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })
                .then(response => {
                    if (!response.ok) {
                        return response.text().then(text => {
                            throw new Error(`API error ${response.status}: ${text}`);
                        });
                    }
                    return response.text();
                })
                .then(result => {
                    alert(result);
                })
                .catch(error => {
                    console.error("Error submitting appointment:", error);
                    alert("Error submitting appointment: " + error.message);
                });
        } catch (error) {
            console.error("Unexpected error:", error);
            alert("Unexpected error: " + error.message);
        }
    };


    function registerPatient() {
        const patientForm = document.getElementById('register-patient-form');
        patientForm.onsubmit = submitPatientData;
    };
    function submitPatientData(e) {

        e.preventDefault();
        const registerPatientForm = document.getElementById('register-patient-form');

        const patientName = registerPatientForm.querySelector('#name');
        const patientPhone = registerPatientForm.querySelector('#phone');
        const patientEmail = registerPatientForm.querySelector('#email');
        const patientRoom = registerPatientForm.querySelector('#room');
        const patientGender = registerPatientForm.querySelector('#gender');
        const doctorId = registerPatientForm.querySelector('#doctorId');
        const admissionStatus = registerPatientForm.querySelector('#admissionstatus');


        const data = {
            name: patientName.value,
            phone: patientPhone.value,
            email: patientEmail.value,
            room: patientRoom.value,
            gender: patientGender.value,
            assignedDoctorId: doctorId.value,
            status: admissionStatus.value

        };

        try {
            fetch('https://localhost:7117/api/Patients', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })
                .then(response => {
                    if (!response.ok) {
                        return response.text().then(text => {
                            throw new Error(`API error ${response.status}: ${text}`);
                        });
                    }
                    return response.text();
                })
                .then(result => {
                    alert(result);
                })
                .catch(error => {
                    console.error("Error submitting paient:", error);
                    alert("Error submitting paient: " + error.message);
                });
        } catch (error) {
            console.error("Unexpected error:", error);
            alert("Unexpected error: " + error.message);
        }
    };
    function preparePatientsTable() {
        const requestPatientsButton = document.getElementById('reception-View-patients');
        requestPatientsButton.onclick = populatePatientsTable;
    };
    async function populatePatientsTable(e) {
        const table = document.getElementById("all-patient-table");
        const tbody = document.getElementById("all-patient-table-body");

        try {

            const response = await fetch("https://localhost:7117/api/Patients");

            if (!response.ok) {
                throw new Error("Failed to fetch patients.");
            }

            const patients = await response.json();


            tbody.innerHTML = "";

            patients.forEach(patient => {
                const row = document.createElement("tr");
                row.innerHTML = `
                        <td>${patient.id}</td>
                        <td>${patient.name}</td>
                        <td>${patient.room}</td>
                        <td>${patient.gender}</td>
                        <td>${patient.assignedDoctorId}</td>
                        <td>${patient.status}</td>
                    `;
                tbody.appendChild(row);
            });

            table.style.display = "table";

        } catch (error) {
            alert("Error loading patients: " + error.message);
            console.error(error);
        }
    };

    function searchPatientByPhoneOrName() {
        const searchBarInput = document.querySelector('#nameInput');

        searchBarInput.addEventListener('keydown', async function (e) {
            if (e.key === 'Enter') {
                const name = e.target.value;

                try {
                    const response = await fetch(`https://localhost:7117/api/Patients/by-name/${name}`, {
                        method: 'GET',
                        headers: { 'Content-Type': 'application/json' }
                    });

                    if (!response.ok) {
                        throw new Error(`HTTP error! Status: ${response.status}`);
                    }

                    const data = await response.json();

                    
                    document.getElementById('patients-table').classList.add('hidden');

                    const patientTable = document.getElementById('all-patient-table');
                    patientTable.innerHTML = '';

                    data.forEach(patient => {
                        const row = document.createElement("tr");
                        row.innerHTML = `
                        <td>${patient.id}</td>
                        <td>${patient.name}</td>
                        <td>${patient.room}</td>
                        <td>${patient.gender}</td>
                        <td>${patient.assignedDoctorId}</td>
                        <td>${patient.status}</td>
                    `;
                        patientTable.appendChild(row);
                    });

                   /* patientTable.style.display = 'table';*/
                } catch (error) {
                    console.error('Fetch error:', error);
                }
            }
        });
    }


}(window.app = window.app || {}));

