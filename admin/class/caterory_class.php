<?php 
include "../database.php";
?>
<?php

class cartgory {
    private $db;

    public function_construct()
    {
        $this -> db = new Database();
    }
    public function insert_caterory($cartgory_name) {
        $query = "INSERT IN tbl_carterory (cartegory_name) VALUES ('$cartgory_name')";
        $result = $this ->db ->insert($query)
    }
}
 
?>