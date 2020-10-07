 
<?php
 
if($_POST["action"] == "post_to_table") {
   mysql_query("SELECT * FROM $_POST[tableId]");
?>
 