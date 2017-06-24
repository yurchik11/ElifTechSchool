Vue.component('item',
{
    template: '#item-template',
    props: {
        model: Object
    },
    data: function() {
        return {
            open: false
        }
    },
    computed: {
        isFolder: function() {
            return this.model.children &&
                this.model.children.length;
        }
    },
    methods: {
        toggle: function() {
            if (this.isFolder) {
                this.open = !this.open;
            }
        },
        changeType: function() {
            if (!this.isFolder) {
                Vue.set(this.model, 'children', []);
                this.addChild();
                this.open = true;
            }
        },
        SaveClickedCompID: function () {
            clickedID = this.model.ID;
            clickedModel = this.model;
        }
    }
});

var clickedID, clickedModel, demo;
GetDataForTree();


function GetDataForTree() {
    PageMethods.GetDataForTree(function (response) {
        demo = new Vue({
            el: '#demo',
            data: {
                treeData: response
            }
        });
    });
}

var saveChangesEl = new Vue({
    el: '.modal-footer',
    data: {
    
    },
    methods: {
        SaveChangesClick: function (event) {
            if ($("#btnSaveAndAdd").text() == "Add new company") {
                var temp =
                {
                    ID: clickedID,
                    Name: $('#Name').val(),
                    Earning: parseInt($('#Earning').val())
                }
                PageMethods.AddNewCompany(temp, function(response) {
                    demo.treeData = response;
                });
            }
            else {
                var temp =
                    {
                        ID: parseInt($('#ID').val()),
                        Name: $('#Name').val(),
                        Earning: parseInt($('#Earning').val())
                    }
                clickedModel.Name = temp.Name;
                clickedModel.Earning = temp.Earning;
                PageMethods.EditCompany(temp);
            }
            $("#closeModalBtn").click();
        }
    }
});

function addNewSubCompany() {
    $('#ID').val(null);
    $('#Name').val(null);
    $('#Earning').val(null);
    $('#btnSaveAndAdd').text("Add new company");
    $('h4.modal-title').html("Add new sub company!");
    $("#showInfoModal").click();
}

$(function () {
    $.contextMenu({
        selector: '.context-menu-one',
        callback: function (key, options) {
            if (key == "add") {
                addNewSubCompany();
            }
            else if (key == "edit") {
                $('#ID').val(clickedModel.ID);
                $('#Name').val(clickedModel.Name);
                $('#Earning').val(clickedModel.Earning);
                $('#btnSaveAndAdd').text("Save changes");
                $('h4.modal-title').html("Edit Company!");
                $("#showInfoModal").click();
            } else {
                var confirmDelete = confirm("Do you want to delete this company?");
                if (confirmDelete) {
                    PageMethods.DeleteCompany(clickedID,function(response) {
                        demo.treeData = response;
                    });
                }
            }

           /* var m = "clicked: " + key;
            window.console && console.log(m) || alert(m);*/
        },
        items: {
            "add": { name: "Add", icon: "fa-plus" },
            "edit": { name: "Edit", icon: "edit" },
            "delete": { name: "Delete", icon: "delete" },
            "sep1": "---------",
            "quit": {
                name: "Quit", icon: function () {
                    return 'context-menu-icon context-menu-icon-quit';
                }
            }
        }
    });

    $('.context-menu-one').on('click',
        function(e) {
            console.log('clicked', this);
        });
});