# ASP-Net-MVC Omnivus
<h3>Functions</h3> 
<li> Create users / log in as user/admin </li>
<li> User roles </li>
<li> Edit profiles </li>
<li> Edit roles </li>
<li> Edit users </li>

<h2>Known bugs: </h2>
<li> In user list -  selected role to edit must equate/be within the list of roles, otherwise the program crashes. I.e typing in a role that is to be edited that doesn't will crash. This is also the case in user Profile </li>
<li>When logged in as Admin and editing user roles on user Profile, after submitting changes and clicking on "edit role" the user is taken to the admin edit profile page. So the admin needs to go back to the user list.</li>

<h2>To do:</h2>
<li>Validation on edit profile did not work as intended when picutre and editing roles was added. Thus, validation is disabled. To solve, this Javascript validation should be added</li>
<li>When signing up, creating roles, or any other inputs - all should be formatted with ToUpper so everything is formatted the same way. I.e no lowercases etc.</li>
<li>Edit some design features so that all is more uniform </li>
