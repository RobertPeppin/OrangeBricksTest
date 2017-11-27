# Purplebricks Developer Test

The aim of this test is to give us an idea about how you approach the development and maintenance of web applications. You will work from a GitHub repository which contains an existing web application. The UI should be functional, but there is no expectation that you modify the brand theme. We are looking for a solution that shows how you build maintainable, scalable and secure software. The test is based on an overly simplified version of our business domain.

The existing web application supports two types of customer. Sellers are able to upload information about a property and list the property for sale. Buyers can search for a property and make an offer. When an offer has been placed on a property the seller should be able to accept or reject the offer.

## Test Objectives

### Objective 1 - Extend an existing feature

We need you to extend the offer functionality of the web application so that when a seller accepts an offer the buyer that placed the offer can see that their offer has been accepted.

**User Story:** *As a buyer I want to see when my offer has been accepted so that I can proceed with the property purchase.*

### Objective 2 – Add a new feature

We need you to add the ability for a buyer to book a viewing. It’s unlikely a customer would want to make an offer on a property without booking a viewing.

**User Story:** *As a buyer I want to book a viewing appointment at a property so that I can determine whether I would like to make an offer.*

### Objective 3 - Review the existing code


Write a short review of the existing sample codebase. Let us know what you think is good or bad about it. Feel free to fix any problems and commit these changes to the solution.

## Results

Notes:

My background of late is on Mobile App, and Windows Store App development so the Web Front part of this test was not that familiar to me. I have designed and developed Azure App Services which follow the same Web API pattern so this was familiar and the Entity Framework parts I was completely at home with. 

### Objective 1

To do this I updated the Offer data item to contain a reference to the Buyer Id. This allowed the code to then lookup offers made by buyers to see the current status.
I also updated the Model object so that Entity Framework could traverse the parent child relationships.
The answer here is very simple and will only allow one offer, but in reality, there is likely to be a series of offers and rejections before an offer is accepted, and a Buyer and Seller will want to be able to see this activity. There is no validation of the offer amount - e.g. is it within a percentage of the advertised price? Is it higher or lower than the previous offer? etc.

### Objective 2

In order to create a very simple viewing capability, the first step was to create a new table to hold details of viewings.  I then created a new operation on the Property controller that would allow a user to start creating a viewing. A simple View Model was created for this new operation, and then a new View was created to provide the user to specify the viewing date and time.
The property rendering for a Buyer was updated to display either the ability to book an appointment if none was recorded or display when the viewing was booked for.

There are a lot of holes in the process here. A graphical appointment booking system would be better showing what days it was possible to book a viewing (this would be based on an availability pattern for the seller), and then what slots are available to be booked (e.g. which were already booked) so we would ensure the viewing wasn’t double booked.

It is also possible that a buyer may book more than one viewing for a property, which the current UI doesn’t allow for (the data structures do)

### Objective 3

As I don’t have a great deal of experience regarding the cshtml side of the code, I am going to leave this aside and focus more on the c sharp code. 

In general the code was written in such a way that I could understand the purpose and intent. I did find that the general lack of comments meant that it did take a couple of readings to appreciate why a method existed.

There are many examples of one class file containing multiple class definitions (See OffersOnPropertyViewModel.cs as an example). I would normally ask developers to make sure classes are defined within their own files for clarity sake. 

With regards to the Data Models (the table definitions). These appear incomplete as they do not possess the virtual properties that enable Entity Framework to navigate PK/FK relationships on the database.
e.g.

In Property.cs

   Public virtual ICollection<Property> Properties{get;set;}
   
In Offer.cs
   
   Public int PropertyId{get;set;}
   Public virtual Property Property {get;set;}

I would also have defined a base class that all table definitions would have inherited from. This would contain the primary key definitions and columns such as the createdat and updatedat entries to reduce code duplication. I would also ask that table definitions such as these were hosted within a separate Folder within the solution for clarity purposes.

Each of the Controllers could also benefit from the creation of base class that would reduce the duplication of storing of the Context.

Naming wise classes, properties, fields etc. are consistently named and clear.
The lack of exception handling is an obvious failing – try creating a property listing without a description. I would also like to see Application Insights integrated so that performance statistics can be easily captured, as well as providing a mechanism for recording exceptions and trace events.

Given the desire to move to a microservice pattern, this web site could be split into several services. 

•	Property Listing

•	Property Browsing

•	Offers

•	Viewings

•	User Management


Each of these could then provide a light weight service that was focussed on only that area. This would reduce the complexity of the individual services i.e. The Offers service would need to have no awareness of the Viewings service. 

The data could be held in different data stores (possibly not a SQL Database but some for of NoSQL storage or Azure Table storage) allowing for individual stores to be optimised and scaled appropriately. 

The Property Browsing Service is likely to be the service with the highest load, so by isolating this we can scale this area without scaling the user management or other parts that do not generate the same load. 

This would also mean that maintenance can be carried out on the Viewings service without taking down the rest of the system.


