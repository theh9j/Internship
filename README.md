1. Kiến Trúc Class
GameManager:
- Xử lý game UI conditions
  - Xử lý hoàn thành level
  - Lưu chữ movement count của Player
- Cấm input khi anim đang chạy
- Hub cho các Managers còn lại (communication)

LevelManager:
- Đọc file JSON lưu trong Resources/Levels/level_0x.json
- Convert data từ JSON sang runtime
- Đọc từ runtime data sang level data
- Gọi visual refresh khi user qua level mới hoặc restart level

GridManager:
- Lưu trữ và load data của puzzle theo 'board'
- Validate hướng của arrow
  - Xóa arrow nếu hướng của arrow không bị obstructed
- Check xem Player qua màn chưa
- Xử lý logic game

GridView:
- Instantiate visual của arrow
  - Spawn arrow theo segment được chưa trong JSON
  - Đảm bảo segment position, rotation, orientation rồi spawn

 FaceGridGenerator:
 - Instantiate tile theo grid
  - Thay đổi tile size theo grid size

FaceGridData:
- Lưu thông tin của transform
- Convert tọa độ theo grid sang tọa độ 3D

InputManager:
- Xử lý mouse click/input
  - Gửi call sang cho GameManager

UIManager:
- Xử lý phần UI

DataModel/Constructs:
- GridCell
- CellData
- LevelData
- LevelCellData
- Direction
- CubeFace
- SegmentType

2. Lời giải
Phần này em ko biết giải thích như thế nào cho dễ hiểu cả, mà cũng đang 5AM rùi nên em mong c hiểu ý e nhé ;-;
Em sẽ gọi mặt của cube + arrow num từ trái sang phải

Level 1:
Top Face Arrow -> Right Face Arrow -> Left Face Arrow -> Back Face Arrow #1 -> Back Face Arrow #2

Level 2:
Front Face Arrow -> Top Face Arrow -> Right Face Arrow -> Left Face Arrow -> Back Face Arrow #1 -> Back Face Arrow #2 -> Bottom Face Arrow

Level 3:
Front Face Arrow -> Left Face Arrow -> Right Face Arrow -> Top Face Arrow #1 -> Top Face Arrow #2 -> Bottom Face Arrow -> Back Face Arrow #1 -> Back Face Arrow #2 -> Back Face Arrow #3

4. Những gì đã làm được
Core logic của game:
- Grid generation
- JSON file level loading
- Xử lý behavior của mũi tên
- Xử lý hoàn thành level
- Tính moves của Player
- Chặn movement khác khi đang interact

Visual/Anim:
- Spawn tiles theo script
- Spawn arrow theo script/json
- Điểu chỉnh rotation, orientation và vị trí của arrow theo từng phần
- Điều chỉnh size của arrow tùy vào grid size (v.d Grid 5x5 arrow sẽ to hơn)
- Support camera orbit vs cả camera zoom
- Anim đơn giản ease-out khi Arrow thoát

UI:
- Hiện Level, Move count
- Hiện LEVEL COMPLETE khi hoàn thành level
- Có nút Restart để load lại level đấy

Misc:
- Tách biệt visual vs logic của game
- Không hardcode level logic

4. Những gì chưa làm được
- Mặc dù Arrow có thể chạy qua các mặt của Cube nma nó khá là buggy khi GridManager đọc ko biết là 2 cái arrow có thực sự nối nhau không
- Anim không support corner anim, basically toàn bộ arrow di chuyển theo một hướng dù có corner piece
