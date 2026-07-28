# PNAD DevTools

Bộ công cụ Editor tích hợp cho Unity, hỗ trợ quy trình khởi tạo dự án nhanh chóng.

## Tính năng

- 🚀 **Init Project** — Tự động tạo cấu trúc thư mục chuẩn `Assets/_Project`
- 📋 **Script Templates** — Cài Script Templates vào `Assets/ScriptTemplates`
- 📄 **Script Generator** — Sinh nhanh script mẫu (UIManager, SoundManager, SaveManager, ...)
- 📦 **Import Package** — Import các package phổ biến trực tiếp qua UPM

---

## Yêu cầu

- Unity **2021.3** trở lên

---

## Cài đặt

### Cách 1 — Package Manager UI *(khuyến nghị)*

1. Mở **Window → Package Manager**
2. Nhấn nút **`+`** (góc trên bên trái)
3. Chọn **Add package from git URL...**
4. Dán URL sau vào và nhấn **Add**:

```
https://github.com/devduc11/dev_tools.git?path=Assets/Packages/com.pnad.devtools#v1.0.0
```

---

### Cách 2 — Sửa `Packages/manifest.json`

Mở file `Packages/manifest.json` trong project và thêm dòng sau vào `dependencies`:

```json
{
  "dependencies": {
    "com.pnad.devtools": "https://github.com/devduc11/dev_tools.git?path=Assets/Packages/com.pnad.devtools#v1.0.0"
  }
}
```

Lưu file, Unity sẽ tự động download và import package.

---

## Sử dụng

Sau khi cài đặt, mở cửa sổ DevTools từ menu:

```
Tools → PNAD DevTools
```

### Tab Init Project

| Nút | Chức năng |
|---|---|
| 🚀 Execute Init Project Structure | Tạo cấu trúc thư mục `Assets/_Project` |
| 📋 Install Script Templates | Copy Script Templates vào `Assets/ScriptTemplates` |

> ⚠️ Sau khi **Install Script Templates**, Unity cần **Restart** để menu `Create` cập nhật. Nhấn nút **🔄 Restart Unity** xuất hiện sau khi cài.

### Tab Script Templates

Sinh nhanh các script mẫu vào đúng thư mục trong `Assets/_Project/Scripts/`.

### Tab Import Package

Import các package phổ biến qua UPM với một cú click.

---

## Changelog

### v1.0.0
- Init Project Structure
- Script Templates (UIManager, SoundManager, SaveManager, ...)
- Import Package tab (EagleSDK, UIEffect, UIParticle)
- Script Template Installer
