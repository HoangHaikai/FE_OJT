# SportsBicycleStore – Design Pattern Demo (.NET 8)

Dự án học tập (OJT – On-the-Job Training) minh họa các **Design Pattern** phổ biến trong C#/.NET thông qua bài toán quản lý cửa hàng xe đạp thể thao.

---

## Mục lục

- [Tổng quan](#tổng-quan)
- [Cấu trúc dự án](#cấu-trúc-dự-án)
- [Kiến trúc tổng thể](#kiến-trúc-tổng-thể)
- [Các Design Pattern được áp dụng](#các-design-pattern-được-áp-dụng)
- [Mô hình dữ liệu (Domain)](#mô-hình-dữ-liệu-domain)
- [Hướng dẫn chạy](#hướng-dẫn-chạy)
- [Thành viên](#thành-viên)

---

## Tổng quan

| Thông tin       | Chi tiết                                  |
|-----------------|-------------------------------------------|
| Ngôn ngữ        | C# (.NET 8 / .NET 10)                     |
| Framework       | .NET SDK                                  |
| Database        | PostgreSQL (host: Render.com)             |
| ORM             | Entity Framework Core 8                   |
| Solution file   | `SportsBicycleStore.slnx`                 |
| Số projects     | 11                                        |

---

## Cấu trúc dự án

```
SportsBicycleStore/          ← Ứng dụng console chính (entry point)
Domain/                      ← Lớp domain: Entities, DTOs, DbContext
Repositories/                ← Lớp truy cập dữ liệu (Repository Pattern)
Services/                    ← Lớp nghiệp vụ (Service Layer)
AdapterPattern/              ← Adapter Pattern (DTO ↔ Entity)
BuilderPattern/              ← Builder Pattern (tạo entity theo kiểu fluent)

── Các project của sinh viên ──
DaoNguyenTrong.DesginPattern/
HaHuyHoang.DesginPattern/
LeQuocViet/
LeQuocViet-Adapter/
VoVanCuong.DesginPattern/
```

### Chi tiết từng layer

```
Domain/
├── Entities/          ← 14 entity EF Core (Mbrand, Mproduct, Morder, Muser, …)
├── DTO/               ← Data Transfer Objects (BrandDTO, CategoryDTO, …)
└── Data/
    └── AppDbContext.cs ← EF Core DbContext + PostgreSQL config

Repositories/
├── InterfaceRepositories/   ← IBrandRepository, ICategoryRepository, …
├── Repositories/            ← BrandRepository, CategoryRepository, …  (in-memory List<T>)
└── RepositoryBase.cs

Services/
├── InterfaceServices/       ← IBrandService, ICategoryService, …
└── Services/                ← BrandService, CategoryService, …

AdapterPattern/
├── IAdapter/                ← IBrandAdapter, ICategoryAdapter, …
└── Adapter/                 ← BrandAdapter, CategoryAdapter, …

BuilderPattern/
├── InterfaceBuilder/        ← IBrandBuilder, ICategoryBuilder, …
└── Builder/                 ← BrandBuilder, CategoryBuilder, …  (fluent API)
```

---

## Kiến trúc tổng thể

```
┌────────────────────────────────────────────────────────┐
│ Presentation Layer  (Program.cs của các project)       │
└──────────────────────────┬─────────────────────────────┘
                           │ gọi
┌──────────────────────────▼─────────────────────────────┐
│ Service Layer  (Services/)                             │
│  IBrandService, ICategoryService, IOrderService, …     │
└────────────┬──────────────────────┬────────────────────┘
             │ dùng                 │ dùng
┌────────────▼───────────┐  ┌───────▼────────────────────┐
│ Adapter Pattern        │  │ Repository Pattern         │
│ DTO → Entity           │  │ Lưu/đọc dữ liệu (List<T>) │
│ (dùng Builder)         │  └────────────────────────────┘
└────────────┬───────────┘
             │ dùng
┌────────────▼───────────┐
│ Builder Pattern        │
│ Fluent entity builder  │
└────────────┬───────────┘
             │ tạo
┌────────────▼───────────────────────────────────────────┐
│ Domain Layer  (Domain/)                                │
│ Entities: Mbrand, Mcategory, Mproduct, Morder, Muser… │
│ DTOs: BrandDTO, CategoryDTO, OrderDTO, …               │
└────────────┬───────────────────────────────────────────┘
             │ EF Core
┌────────────▼───────────────────────────────────────────┐
│ PostgreSQL Database (Render.com)                       │
└────────────────────────────────────────────────────────┘
```

---

## Các Design Pattern được áp dụng

### 1. Builder Pattern

Xây dựng đối tượng Entity theo kiểu **fluent interface**, giúp tạo object phức tạp từng bước.

```csharp
// BrandBuilder.cs
public IBrandBuilder SetId(string id)        { _brand.BrandId = id; return this; }
public IBrandBuilder SetName(string name)    { _brand.BrandName = name; return this; }
public IBrandBuilder SetDescription(string d){ _brand.Description = d; return this; }
public IBrandBuilder SetIsActive(bool v)     { _brand.IsActive = v; return this; }
public Mbrand Build()                        { return _brand; }
```

### 2. Adapter Pattern

Chuyển đổi **DTO** (nhận từ UI/API) thành **Entity** (lưu vào DB) thông qua Builder.

```csharp
// BrandAdapter.cs
public Mbrand ConvertToEntity(BrandDTO dto)
{
    return _builder
        .SetId(dto.BrandId)
        .SetName(dto.BrandName)
        .SetDescription(dto.Description)
        .SetIsActive(true)
        .SetCreatedAt(DateTime.Now)
        .Build();
}
```

### 3. Repository Pattern

Trừu tượng hóa lớp truy cập dữ liệu. Hiện tại dùng `List<T>` in-memory cho mục đích demo.

```csharp
// IBrandRepository.cs
public interface IBrandRepository {
    void Add(Mbrand brand);
    void Update(Mbrand brand);
    List<Mbrand> GetAll();
}
```

### 4. Service Layer (Facade)

Đóng gói logic nghiệp vụ, kết hợp Repository + Adapter.

```csharp
// Cách sử dụng trong Program.cs
IBrandRepository repo    = new BrandRepository();
IBrandAdapter    adapter = new BrandAdapter();
IBrandService    service = new BrandService(repo, adapter);

service.CreateBrand(new BrandDTO { BrandId = "B01", BrandName = "Trek" });
service.GetAll(); // trả về List<Mbrand>
```

---

## Mô hình dữ liệu (Domain)

| Entity               | Mô tả                          |
|----------------------|--------------------------------|
| `Mbrand`             | Thương hiệu xe đạp             |
| `Mcategory`          | Danh mục sản phẩm              |
| `Mproduct`           | Sản phẩm (xe đạp)              |
| `Mlisting`           | Bài đăng bán sản phẩm          |
| `Morder`             | Đơn hàng                       |
| `Morderdetail`       | Chi tiết đơn hàng              |
| `Mpayment`           | Thanh toán                     |
| `Muser`              | Người dùng                     |
| `Mrole`              | Vai trò người dùng             |
| `Mwishlist`          | Danh sách yêu thích            |
| `Mmessage`           | Tin nhắn                       |
| `Mdispute`           | Khiếu nại / tranh chấp         |
| `MdisputeEvidence`   | Bằng chứng tranh chấp          |
| `Minspectionreport`  | Báo cáo kiểm định sản phẩm     |

---

## Hướng dẫn chạy

### Yêu cầu

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) trở lên
- PostgreSQL (hoặc dùng kết nối Render.com đã cấu hình sẵn)

### Chạy project mẫu

```bash
# Clone repo
git clone https://github.com/HoangHaikai/FE_OJT.git
cd FE_OJT

# Chạy project mẫu của sinh viên (DaoNguyenTrong)
dotnet run --project DaoNguyenTrong.DesginPattern

# Hoặc chạy ứng dụng chính
dotnet run --project SportBicycleStore
```

### Build toàn bộ solution

```bash
dotnet build SportsBicycleStore.slnx
```

---

## Thành viên

| Tên             | Project                         | Framework  |
|-----------------|---------------------------------|------------|
| Đào Nguyễn Trọng | `DaoNguyenTrong.DesginPattern` | .NET 10    |
| Hà Huy Hoàng    | `HaHuyHoang.DesginPattern`     | .NET 10    |
| Lê Quốc Việt    | `LeQuocViet` / `LeQuocViet-Adapter` | .NET 8 |
| Võ Văn Cường    | `VoVanCuong.DesginPattern`     | .NET 10    |
