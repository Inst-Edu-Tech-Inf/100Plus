<?php 
    
	$servername = "s140.cyber-folks.pl";
	$username = "kolacz_zdalny";
	$password = "SummOn2020.";
	$dbname = "kolacz_jos1";
	$socket = "3306";
	$nauczycielPass = $_POST["nauczycielPass"];
	$aktywnaKlasa = $_POST["aktywnaKlasa"];
	$nrSzkoly = "";
	$licznik = 0;
	//
	
	//create connection
	$conn = new mysqli($servername, $username, $password, $dbname);
	
	//check connection
	if ($conn->connect_error) {
		die("Connection failed: " . $conn->connect_error);
	}
	//echo "Connected successfully";
	
	$sql = "SELECT szkola FROM `nauczyciel` WHERE `identyfikator` LIKE \"$nauczycielPass\" ";	
	
	$result = $conn->query($sql);
	
	if ($result->num_rows > 0){
		while($row = $result->fetch_assoc()) {
			//echo $row["szkola"];
			//echo "<br>";
			$nrSzkoly = $row["szkola"];
		}
		
	
		$sql = "SELECT ucz1, ucz2, ucz3, ucz4, ucz5, ucz6, ucz7, ucz8, ucz9, ucz10, ucz11, ucz12, ucz13, ucz14, ucz15, ucz16, ucz17, ucz18, ucz19, ucz20, ucz21, ucz22, ucz23, ucz24, ucz25, ucz26, ucz27, ucz28, ucz29, ucz30, ucz31, ucz32, ucz33, ucz34, ucz35, ucz36, ucz37, ucz38, ucz39, ucz40, u1kod, u2kod, u3kod, u4kod, u5kod, u6kod, u7kod, u8kod, u9kod, u10kod, u11kod, u12kod, u13kod, u14kod, u15kod, u16kod, u17kod, u18kod, u19kod, u20kod, u21kod, u22kod, u23kod, u24kod, u25kod, u26kod, u27kod, u28kod, u29kod, u30kod, u31kod, u32kod, u33kod, u34kod, u35kod, u36kod, u37kod, u38kod, u39kod, u40kod  FROM `klasa` WHERE `szkola` LIKE \"$nrSzkoly\";";
		
		//$sql = "SELECT COUNT(*) as total FROM `szkola`";
		
		$result = $conn->query($sql);

		if ($result->num_rows > 0){
			while($row = $result->fetch_assoc()) {
				//echo $row["szkola"];
				//
				if ($aktywnaKlasa == $licznik) {
					if ($row["ucz1"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz1"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}							
						echo "1.".$row["u1kod"];
						echo "</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz2"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz2"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "2.".$row["u2kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz3"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz3"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "3.".$row["u3kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz4"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz4"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "4.".$row["u4kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz5"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz5"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "5.".$row["u5kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz6"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz6"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "6.".$row["u6kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz7"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz7"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "7.".$row["u7kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz8"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz8"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "8.".$row["u8kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz9"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz9"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "9.".$row["u9kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz10"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz10"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "10.".$row["u10kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz11"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz11"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "11.".$row["u11kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz12"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz12"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "12.".$row["u12kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz13"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz13"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "13.".$row["u13kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz14"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz14"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "14.".$row["u14kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz15"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz15"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "15.".$row["u15kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz16"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz16"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "16.".$row["u16kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz17"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz17"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "17.".$row["u17kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz18"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz18"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "18.".$row["u18kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz19"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz19"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "19.".$row["u19kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz20"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz20"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "20.".$row["u20kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz21"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz21"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "21.".$row["u21kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz22"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz22"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "22.".$row["u22kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz23"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz23"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "23.".$row["u23kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz24"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz24"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "24.".$row["u24kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz25"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz25"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "25.".$row["u25kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz26"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz26"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "26.".$row["u26kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz27"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz27"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "27.".$row["u27kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz28"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz28"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "28.".$row["u28kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz29"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz29"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "29.".$row["u29kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz30"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz30"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "30.".$row["u30kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz31"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz31"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "31.".$row["u31kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz32"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz32"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "32.".$row["u32kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz33"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz33"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "33.".$row["u33kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz34"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz34"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "34.".$row["u34kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz35"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz35"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "35.".$row["u35kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz36"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz36"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "36.".$row["u36kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz37"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz37"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "37.".$row["u37kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz38"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz38"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "38.".$row["u38kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz39"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz39"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "39.".$row["u39kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
					if ($row["ucz40"] == "0"){
						echo "0 ";
					}
					else{
						$nrUcznia = $row["ucz40"];
						$sql2 = "SELECT identyfikator FROM `uczen` WHERE `id` LIKE \"$nrUcznia\";";	
						//echo $sql2;
						$result2 = $conn->query($sql2);	
						$row2 = $result2->fetch_assoc();
							if ($row2["identyfikator"] == "")
							{
								echo "<color=red>";
							}
							else{
								echo "<color=green>";
							}
						echo "40.".$row["u40kod"]."</color>\n ";
					}
					//echo " ";//"<br>";
				}
				$licznik = $licznik + 1;
			}
		}
		else{
			echo "false";
		}
	}
	
	$conn->close();
	
?>