# ASP-Net-MVC Omnivus
<h3>Functions</h3> 
<li> Create users / log in as user/admin </li>
<li> User roles </li>
<li> Edit profiles </li>
<li> Edit roles </li>
<li> Edit users </li>

<h2>Known bugs: </h2>
<li>When editing a users role and the enter value doesn't reflect a value in the role list then the program crashes. I.e Chanings roles must equate roles that are within the role list</li>
<li> In user list -  selected role to edit must equate/be within the list of roles, otherwise the program crashes. I.e typing in a role that is to be edited that doesn't will crash </li>
<li>When logged in as Admin and editing user roles, after submitting changes and clicking on "edit role" the user is taken to the admin edit profile page. So the admin needs to go back to the user list.</li>



<h3>Getting started:</h3>
To create an order: </br>
  A customer needs to exist in the database (Customer - POST) </br>
  A product needs to exist in the database (Product -  POST) </br>
  A status needs to exist in the database (Status - POST) </br>
<h3>Known bugs</h3>
When updating the price and name of a product (PUT), existings <strong>orders</strong> will also have their price and product name changed, however the line price does not reflect the change. This is a bug and not intentional. Updating a product should not make any changes in old/existing orders. <strong>orders</strong> created after the product has been edited work as intended.
