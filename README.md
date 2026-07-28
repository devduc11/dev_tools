# DevTools Project

Dự án chứa Unity Editor Package **PNAD DevTools** (`com.pnad.devtools`).

## 📦 Cách cài đặt PNAD DevTools vào dự án mới qua Unity Package Manager (UPM)

Mở Unity Editor ở dự án mới, chọn **Window → Package Manager → (+) Add package from git URL...** và dán đường link sau:

```text
https://github.com/devduc11/dev_tools.git?path=Assets/Packages/com.pnad.devtools#main
```

Hoặc thêm vào file `Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.pnad.devtools": "https://github.com/devduc11/dev_tools.git?path=Assets/Packages/com.pnad.devtools#main"
  }
}
```

---

## 🛠️ Tính năng của PNAD DevTools (`com.pnad.devtools`)

- 🚀 **Init Project**: Khởi tạo cấu trúc thư mục chuẩn `Assets/_Project`
- 📋 **Script Templates**: Cài đặt Script Templates vào `Assets/ScriptTemplates`
- 📄 **Script Generator**: Sinh nhanh script mẫu (UIManager, SoundManager, SaveManager, ...)
- 📦 **Import Package**: Import các package phổ biến (EagleSDK, UIEffect, UIParticle)
