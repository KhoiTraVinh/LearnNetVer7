//Why use
#Advantages
--MQ có thể làm trung gian cho nhiều service khác sử dụng -> lợi ích là có thể tách nhiều service
--Có thể chia làm nhiều task bất đồng bộ -> giảm thời gian thực thi
--giúp giảm áp lực truy vấn xuống sql -> tránh việc sập server
#defect
--Tăng độ phức tạp của code
--Có thể bị miss req
--Vì bất đồng bộ nên có thể bị miss task