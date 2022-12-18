//Why use
</br>
#Advantages
</br>
--MQ có thể làm trung gian cho nhiều service khác sử dụng -> lợi ích là có thể tách nhiều service
</br>
--Có thể chia làm nhiều task bất đồng bộ -> giảm thời gian thực thi
</br>
--giúp giảm áp lực truy vấn xuống sql -> tránh việc sập server
</br>
#defect
</br>
--Tăng độ phức tạp của code
</br>
--Có thể bị miss req
</br>
--Vì bất đồng bộ nên có thể bị miss task
</br>
</br>
</br>
//Redis
</br>
--Các từ khóa cơ bản:
</br>
+set namekey "value" -> set 1 value cho 1 key
</br>
+get namekey -> lấy value của 1 key
</br>
+getrange namekey start end -> lấy value của 1 key trong 1 khoảng nhất định (từ trái qua phải 0->n+1(n=0)):(ngược lại -1->n-1(n=-1))
</br>
+mset namekey1 value1 namekey2 value2,... -> set nhiều key value cùng lúc
</br>
+mget namekey1 namekey2,... -> get value nhiều key cùng lúc
</br>
+strlen namekey -> lấy độ dài của chuỗi value
</br>
+incr namekey -> tăng value lên 1 đơn vị
</br>
+incrby namekey sotang -> tăng value lên nhiều đơn vị
</br>
+decr namekey -> giảm value xuống 1 đơn vị
</br>
+decrby namekey sogiam -> giảm value xuống nhiều đơn vị
</br>
+expire namkey timetolive -> set thời gian hết hạn của 1 giá trị của key
</br>
+ttl namekey -> get thời gian hết hạn cho 1 giá trị của key