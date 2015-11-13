var customerRegisterViewModel;

// use as register customer views view model
function Customer(customerid, customerName, contactName, address, city, postalcode, country) {
    var self = this;

    // observable are update elements upon changes, also update on element data changes [two way binding]
    self.CustomerID = ko.observable(customerid);
    self.CustomerName = ko.observable(customerName).extend({ required: true });
    self.ContactName = ko.observable(contactName).extend({ required: true });
    self.Address = ko.observable(address).extend({ required: true });
    self.City = ko.observable(city).extend({ required: true });
    self.PostalCode = ko.observable(postalcode).extend({ required: true });
    self.Country = ko.observable(country).extend({ required: true });

    self.addCustomer = function () {
        var dataObject = ko.toJSON(this);

        $.ajax({
            url: '/api/customer',
            type: 'post',
            data: dataObject,
            contentType: 'application/json',
            success: function (data) {
                customerRegisterViewModel.customerListViewModel.customers.push(new Customer(data.CustomerID, data.CustomerName, data.ContactName, data.Address, data.City, data.PostalCode, data.Country));
                self.CustomerID(null);
                self.CustomerName('');
                self.ContactName('');
                self.Address('');
                self.City('');
                self.PostalCode('');
                self.Country('');
            }
        });
    };
}

// use as customer list view's view model
function CustomerList() {

    var self = this;

    // observable arrays are update binding elements upon array changes
    self.customers = ko.observableArray([]);

    self.getCustomers = function () {
        self.customers.removeAll();

        // retrieve customers list from server side and push each object to model's customers list
        $.getJSON('/api/customer', function (data) {
            $.each(data, function (key, value) {
                self.customers.push(new Customer(value.CustomerID, value.CustomerName, value.ContactName, value.Address, value.City, value.PostalCode, value.Country));
            });
        });
    };


    // remove customer. current data context object is passed to function automatically.
    self.removeCustomer = function (customer) {
        $.ajax({
            url: '/api/customer/' + customer.CustomerID(),
            type: 'delete',
            contentType: 'application/json',
            success: function () {
                self.customers.remove(customer);
            }
        });
    };
}


// create index view view model which contain two models for partial views
customerRegisterViewModel = { addCustomerViewModel: new Customer(), customerListViewModel: new CustomerList() };


// on document ready
$(document).ready(function () {

    // bind view model to referring view
    ko.applyBindings(customerRegisterViewModel);

    // load customer data
    customerRegisterViewModel.customerListViewModel.getCustomers();
});