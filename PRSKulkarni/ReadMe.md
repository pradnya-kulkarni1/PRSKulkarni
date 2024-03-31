## Project Title
Purchase Request System


The Purchase Request System (PRS) allows user to purchase the products for office stationery and equipment.It also allows user to view the summary of all purchases.

### Project mentors 

The project design is by Sean Blessing and Web API guidance is by Mike Smith.

### Description

The User can make a purchase request for stationery items like Papers, Pen, Furniture and equipments like Laptop, printer, electrical items.

Once the user places the order, the admin user can approve or reject the request.

	The requiesting user can view the summary of all requests. The summary includes total amount for each request, price, quantity of the product.

### Getting Started

### Prerequisites

	* Windows 10/11
	* Microsoft SQL Database
	* Microsoft .NET core 
	* Microsoft .ASPNETCore framework. 
	* IIS Web Server
	* Microsoft Edge Web browser.

### Installing
	Download PurchaseRequestSystem.exe to c:/repos

	Open windows command prompt and run PurchaseRquestSytem.exe 

### Detail Design

This is a web application for Purchase Request System. It has three components:

Database, WebServer / Business Logic and Client / Web Browser
 
The database contains following tables: 
User, Vendor, Product, Request, and LineItem.

Business Logic contains following methods

1. Login :Authenticates a user by username and password combination. This method accepts the username and password as argument. 
If the Username and Password matches with the database records, it returns the User; otherwise returns a message indicating User not found.

2. Review Request - If the total amount of request is above $50, the request status is set to Review.

3. Approve Request - If the total amount of request is $50 and less, it gets approved. The admin can also approve the request.

4. Reject Request - Admin can reject the request with a reason for rejection.

5. Generated methods are Post, Get, Delete and Put User, Vendor, Product and Request.



