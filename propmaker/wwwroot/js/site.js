var propmaker = {
    version: "1.0.0.0",
    controls: {
        title: document.getElementById('title'), 
        overview: document.getElementById('overviewText'),
        customerName: document.getElementById('custName'), 
        contactName: document.getElementById('contactName'),
        contactEmail: document.getElementById('contactEmail'), 
        fileName: document.getElementById('filename'), 
        resetButton: document.getElementById('resetButton'), 
        addNewButton: document.getElementById('addNewButton'), 
        loadHeaderButton: document.getElementById('loadHeaderButton'), 
        loadSectionsButton: document.getElementById('loadSectionsButton'), 
        downloadPDFButton: document.getElementById('downloadPDFButton')
    },
    actions: {
        addNewProposal: function () {
            console.log('add new proposal');
            $.ajax({
                url: '/MakerAPI/NewProposal/0', 
                type: 'POST', 
                success: function (data) {

                    console.log({ data });
                    alert('reload');

                    // location.reload();
                }
            });
        }, 
        loadHeader: function () {
            console.log('load header');

        }, 
        loadSections: function () {
            console.log('load sections');


        }, 
        resetForm: function () {
            console.log('reset form');

            $(propmaker.controls.title).val("");
            $(propmaker.controls.overview).val("");
            $(propmaker.controls.customerName).val("");
            $(propmaker.controls.contactEmail).val("");
            $(propmaker.controls.contactName).val("");
            $(propmaker.controls.fileName).val("proposal.docx");

        }
    },
    setup: function () {
        console.log('setup');

        propmaker.controls.resetButton.addEventListener("click", propmaker.actions.resetForm);
        propmaker.controls.addNewButton.addEventListener("click", propmaker.actions.addNewProposal);
        propmaker.controls.loadHeaderButton.addEventListener("click", propmaker.actions.loadHeader);
        propmaker.controls.loadSectionsButton.addEventListener("click", propmaker.actions.loadSections);
        
        console.log('setup complete');
    }
}
// Proposal Maker scripts
console.log(`proposal maker v.${propmaker.version}`);
propmaker.setup();