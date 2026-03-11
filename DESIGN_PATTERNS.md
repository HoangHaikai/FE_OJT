# Giải thích Design Pattern trong dự án SportsBicycleStore

Tài liệu này giải thích chi tiết từng **Design Pattern** được áp dụng trong dự án, kèm theo ví dụ code thực tế từ source code.

---

## Mục lục

1. [Builder Pattern](#1-builder-pattern)
2. [Adapter Pattern](#2-adapter-pattern)
3. [Repository Pattern](#3-repository-pattern)
4. [Service Layer / Facade Pattern](#4-service-layer--facade-pattern)
5. [DTO Pattern](#5-dto-pattern)
6. [Dependency Injection (Bonus)](#6-dependency-injection-bonus)
7. [Luồng xử lý tổng thể](#7-luồng-xử-lý-tổng-thể)
8. [Tóm tắt so sánh các Pattern](#8-tóm-tắt-so-sánh-các-pattern)

---

## 1. Builder Pattern

### Khái niệm

**Builder Pattern** là một **Creational Design Pattern** (mẫu thiết kế khởi tạo) giúp **xây dựng đối tượng phức tạp từng bước** thông qua một giao diện fluent (có thể nối chuỗi phương thức).

### Vấn đề giải quyết

Giả sử bạn cần tạo một đối tượng `Morder` (Đơn hàng) với **14 thuộc tính** khác nhau:

```csharp
// ❌ KHÔNG dùng Builder: khó đọc, dễ nhầm thứ tự tham số
var order = new Morder("ORD001", "buyer1", "seller1", 500000m,
    "123 Lê Lợi, TP.HCM", "Nguyễn Văn A", "0901234567",
    1, 0, 0, "Giao nhanh", DateTime.Now, null, null, null);
```

```csharp
// ✅ Dùng Builder: rõ ràng, dễ bảo trì
var order = builder
    .SetOrderId("ORD001")
    .SetBuyerId("buyer1")
    .SetSellerId("seller1")
    .SetTotalAmount(500000m)
    .SetShippingAddress("123 Lê Lợi, TP.HCM")
    .SetReceiverName("Nguyễn Văn A")
    .SetReceiverPhone("0901234567")
    .SetDeliveryMethod(1)
    .SetOrderStatus(0)    // Pending
    .SetPaymentStatus(0)  // Unpaid
    .SetNote("Giao nhanh")
    .SetCreatedAt(DateTime.Now)
    .Build();
```

### Cấu trúc trong dự án

```
BuilderPattern/
├── InterfaceBuilder/
│   ├── IBrandBuilder.cs      ← interface fluent cho Mbrand
│   ├── ICategoryBuilder.cs   ← interface fluent cho Mcategory
│   ├── IOrderBuilder.cs      ← interface fluent cho Morder
│   └── IWishlistBuilder.cs   ← interface fluent cho Mwishlist
└── Builder/
    ├── BrandBuilder.cs       ← hiện thực IBrandBuilder
    ├── CategoryBuilder.cs    ← hiện thực ICategoryBuilder
    ├── OrderBuilder.cs       ← hiện thực IOrderBuilder
    └── WishlistBuilder.cs    ← hiện thực IWishlistBuilder
```

### Interface Builder

```csharp
// BuilderPattern/InterfaceBuilder/IBrandBuilder.cs
public interface IBrandBuilder
{
    IBrandBuilder SetId(string id);
    IBrandBuilder SetName(string name);
    IBrandBuilder SetDescription(string description);
    IBrandBuilder SetIsActive(bool isActive);
    IBrandBuilder SetCreatedAt(DateTime date);
    IBrandBuilder SetUpdatedAt(DateTime date);
    Mbrand Build();  // ← trả về đối tượng đã xây dựng xong
}
```

> 💡 **Lưu ý quan trọng:** Mỗi phương thức `SetXxx()` trả về `IBrandBuilder` (chính nó), cho phép **method chaining** (nối chuỗi gọi). Phương thức `Build()` là phương thức duy nhất trả về đối tượng thực sự.

### Hiện thực Builder

```csharp
// BuilderPattern/Builder/BrandBuilder.cs
public class BrandBuilder : IBrandBuilder
{
    private Mbrand _brand;

    public BrandBuilder()
    {
        _brand = new Mbrand();  // Khởi tạo object rỗng
    }

    public IBrandBuilder SetId(string id)
    {
        _brand.BrandId = id;
        return this;  // ← trả về this để nối tiếp
    }

    public IBrandBuilder SetName(string name)
    {
        _brand.BrandName = name;
        return this;
    }

    // ... các setter khác tương tự ...

    public Mbrand Build()
    {
        return _brand;  // ← trả về đối tượng hoàn chỉnh
    }
}
```

```csharp
// BuilderPattern/Builder/OrderBuilder.cs - Builder cho đối tượng phức tạp hơn
public class OrderBuilder : IOrderBuilder
{
    private Morder _order;

    public OrderBuilder() { _order = new Morder(); }

    public IOrderBuilder SetOrderId(string orderId)   { _order.OrderId = orderId; return this; }
    public IOrderBuilder SetBuyerId(string buyerId)   { _order.BuyerId = buyerId; return this; }
    public IOrderBuilder SetSellerId(string sellerId) { _order.SellerId = sellerId; return this; }
    public IOrderBuilder SetTotalAmount(decimal amt)  { _order.TotalAmount = amt; return this; }
    public IOrderBuilder SetOrderStatus(int status)   { _order.OrderStatus = status; return this; }
    // ... nhiều setter khác ...

    public Morder Build()
    {
        var result = _order;
        _order = new Morder(); // ← reset để tránh tái sử dụng nhầm
        return result;
    }
}
```

### Khi nào dùng Builder Pattern?

| Tình huống | Có nên dùng Builder? |
|---|---|
| Object có nhiều thuộc tính (≥ 5) | ✅ Nên dùng |
| Một số thuộc tính là tùy chọn (optional) | ✅ Nên dùng |
| Cần đặt giá trị mặc định cho một số field | ✅ Nên dùng |
| Object đơn giản với 1-2 thuộc tính | ❌ Không cần |

---

## 2. Adapter Pattern

### Khái niệm

**Adapter Pattern** là một **Structural Design Pattern** (mẫu thiết kế cấu trúc) đóng vai trò như một **bộ chuyển đổi** giữa hai interface không tương thích với nhau.

Trong dự án này, Adapter chuyển đổi **DTO** (Data Transfer Object – dữ liệu từ UI/API) thành **Entity** (đối tượng domain để lưu vào database).

### Vấn đề giải quyết

```
UI/API nhận vào:    BrandDTO { BrandId, BrandName, Description }
Database cần:       Mbrand  { BrandId, BrandName, Description, IsActive, CreatedAt, UpdatedAt, ... }
```

→ Adapter lo việc **điền thêm các giá trị mặc định** (IsActive = true, CreatedAt = DateTime.Now) mà UI không cần biết.

### Cấu trúc trong dự án

```
AdapterPattern/
├── IAdapter/
│   ├── IBrandAdapter.cs      ← interface: BrandDTO → Mbrand
│   ├── ICategoryAdapter.cs   ← interface: CategoryDTO → Mcategory
│   ├── IOrderAdapter.cs      ← interface: OrderDTO → Morder
│   └── IWishlistAdapter.cs   ← interface: JSON → Mwishlist
└── Adapter/
    ├── BrandAdapter.cs       ← hiện thực IBrandAdapter
    ├── CategoryAdapter.cs    ← hiện thực ICategoryAdapter
    ├── OrderAdapter.cs       ← hiện thực IOrderAdapter
    └── WishlistAdapter.cs    ← hiện thực IWishlistAdapter
```

### Interface Adapter

```csharp
// AdapterPattern/IAdapter/IBrandAdapter.cs
public interface IBrandAdapter
{
    Mbrand ConvertToEntity(BrandDTO dto);  // ← DTO → Entity
}
```

### Hiện thực Adapter

```csharp
// AdapterPattern/Adapter/BrandAdapter.cs
public class BrandAdapter : IBrandAdapter
{
    private readonly IBrandBuilder _builder;

    public BrandAdapter()
    {
        _builder = new BrandBuilder();  // ← sử dụng Builder bên trong
    }

    public Mbrand ConvertToEntity(BrandDTO dto)
    {
        return _builder
            .SetId(dto.BrandId)           // ← copy từ DTO
            .SetName(dto.BrandName)        // ← copy từ DTO
            .SetDescription(dto.Description) // ← copy từ DTO
            .SetIsActive(true)             // ← giá trị mặc định (UI không cần biết)
            .SetCreatedAt(DateTime.Now)    // ← tự động set thời gian (UI không cần biết)
            .Build();
    }
}
```

```csharp
// AdapterPattern/Adapter/OrderAdapter.cs - Adapter cho đối tượng phức tạp
public class OrderAdapter : IOrderAdapter
{
    private readonly IOrderBuilder _builder;

    public OrderAdapter() { _builder = new OrderBuilder(); }

    public Morder ConvertToEntity(OrderDTO dto)
    {
        return _builder
            .SetOrderId(dto.OrderId)
            .SetBuyerId(dto.BuyerId)
            .SetSellerId(dto.SellerId)
            .SetTotalAmount(dto.TotalAmount)
            .SetShippingAddress(dto.ShippingAddress)
            .SetReceiverName(dto.ReceiverName)
            .SetReceiverPhone(dto.ReceiverPhone)
            .SetDeliveryMethod(dto.DeliveryMethod)
            .SetOrderStatus(0)      // ← Pending (mặc định khi tạo mới)
            .SetPaymentStatus(0)    // ← Unpaid (mặc định khi tạo mới)
            .SetCreatedAt(DateTime.Now)
            .Build();
    }
}
```

### Mối quan hệ Adapter – Builder

```
BrandDTO (input)
     ↓
BrandAdapter.ConvertToEntity(dto)
     ↓ (gọi)
BrandBuilder
     ↓ .SetId() .SetName() ... .Build()
Mbrand (output / Entity)
```

> 💡 **Adapter** quyết định **cái gì** cần set.
> **Builder** quyết định **cách** tạo object.

### Khi nào dùng Adapter Pattern?

| Tình huống | Có nên dùng Adapter? |
|---|---|
| Cần chuyển đổi giữa hai kiểu dữ liệu khác nhau | ✅ Nên dùng |
| Muốn cô lập logic mapping ra khỏi business logic | ✅ Nên dùng |
| Tích hợp thư viện bên thứ ba có interface khác | ✅ Nên dùng |
| Hai kiểu dữ liệu hoàn toàn giống nhau | ❌ Không cần |

---

## 3. Repository Pattern

### Khái niệm

**Repository Pattern** là một **Architectural Pattern** (mẫu kiến trúc) giúp **trừu tượng hóa lớp truy cập dữ liệu**. Thay vì code business logic trực tiếp với database, mọi thao tác dữ liệu đều đi qua một lớp Repository.

### Vấn đề giải quyết

```csharp
// ❌ KHÔNG dùng Repository: Service phụ thuộc trực tiếp vào database
public class BrandService
{
    public void CreateBrand(BrandDTO dto)
    {
        // Service biết chi tiết về database! Khó test, khó thay đổi DB
        using var conn = new NpgsqlConnection("...");
        conn.Open();
        var cmd = new NpgsqlCommand("INSERT INTO mbrand VALUES (@id, @name)", conn);
        cmd.Parameters.AddWithValue("id", dto.BrandId);
        // ...
    }
}
```

```csharp
// ✅ Dùng Repository: Service không biết database là gì
public class BrandService
{
    private readonly IBrandRepository _repository; // ← chỉ biết interface

    public void CreateBrand(BrandDTO dto)
    {
        var entity = _adapter.ConvertToEntity(dto);
        _repository.Add(entity); // ← không quan tâm lưu vào đâu
    }
}
```

### Cấu trúc trong dự án

```
Repositories/
├── InterfaceRepositories/
│   ├── IBrandRepository.cs      ← interface CRUD cho Mbrand
│   ├── ICategoryRepository.cs   ← interface CRUD cho Mcategory
│   ├── IOrderRepository.cs      ← interface CRUD cho Morder
│   └── IMWishlistRepository.cs  ← interface CRUD cho Mwishlist
├── Repositories/
│   ├── BrandRepository.cs       ← hiện thực in-memory (List<T>)
│   ├── CategoryRepository.cs    ← hiện thực in-memory
│   ├── OrderRepository.cs       ← hiện thực in-memory
│   └── MWishlistRepository.cs   ← hiện thực EF Core (PostgreSQL)
└── RepositoryBase.cs            ← base class generic với EF Core
```

### Interface Repository

```csharp
// Repositories/InterfaceRepositories/IBrandRepository.cs
public interface IBrandRepository
{
    void Add(Mbrand brand);          // Thêm mới
    void Update(Mbrand brand);       // Cập nhật
    Mbrand GetById(string id);       // Lấy theo ID
    List<Mbrand> GetAll();           // Lấy tất cả
}
```

### Hiện thực In-Memory (dùng cho demo / unit test)

```csharp
// Repositories/Repositories/BrandRepository.cs
public class BrandRepository : IBrandRepository
{
    private List<Mbrand> _brands = new List<Mbrand>(); // ← lưu trong RAM

    public void Add(Mbrand brand)
    {
        _brands.Add(brand);
    }

    public void Update(Mbrand brand)
    {
        var existing = GetById(brand.BrandId);
        if (existing != null)
        {
            existing.BrandName  = brand.BrandName;
            existing.Description = brand.Description;
            existing.IsActive   = brand.IsActive;
            existing.UpdatedAt  = DateTime.Now;
        }
    }

    public Mbrand GetById(string id)
    {
        return _brands.FirstOrDefault(x => x.BrandId == id);
    }

    public List<Mbrand> GetAll()
    {
        return _brands;
    }
}
```

### Hiện thực EF Core (dùng cho production với PostgreSQL)

```csharp
// Repositories/RepositoryBase.cs (dạng generic)
public class RepositoryBase<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public RepositoryBase(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public void Create(T entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges(); // ← lưu vào PostgreSQL
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
        _context.SaveChanges();
    }
}
```

### Lợi ích của Repository Pattern

```
Lớp Service chỉ cần biết interface IBrandRepository
           ↓
Bạn có thể hoán đổi hiện thực bất cứ lúc nào:

IBrandRepository ──► BrandRepository (List<T>, cho demo)
                 ──► BrandEfRepository (EF Core, cho production)
                 ──► BrandMongoRepository (MongoDB, nếu đổi DB)
                 ──► BrandRepositoryMock (Mock, cho unit test)
```

### Khi nào dùng Repository Pattern?

| Tình huống | Có nên dùng? |
|---|---|
| Muốn tách biệt business logic và data access | ✅ Nên dùng |
| Muốn dễ dàng unit test (mock repository) | ✅ Nên dùng |
| Có thể cần đổi database trong tương lai | ✅ Nên dùng |
| Ứng dụng cực kỳ đơn giản, 1 nơi truy cập DB | ❌ Có thể không cần |

---

## 4. Service Layer / Facade Pattern

### Khái niệm

**Service Layer** (còn gọi là **Facade Pattern** trong ngữ cảnh này) là lớp nằm giữa Presentation (UI) và Data Access (Repository). Nó **đóng gói toàn bộ logic nghiệp vụ** và điều phối các thành phần khác.

> **Facade Pattern** (Mẫu mặt tiền): Cung cấp một interface đơn giản, dễ dùng cho một hệ thống con phức tạp.

### Vấn đề giải quyết

```csharp
// ❌ KHÔNG dùng Service Layer: Program.cs biết quá nhiều
public static void Main()
{
    var builder = new BrandBuilder();
    var adapter = new BrandAdapter(builder);
    var entity = adapter.ConvertToEntity(dto);  // Phải tự gọi adapter
    var repo = new BrandRepository();
    repo.Add(entity);  // Phải tự gọi repository
    // UI phải hiểu toàn bộ luồng xử lý!
}
```

```csharp
// ✅ Dùng Service Layer: Program.cs chỉ cần gọi 1 method
public static void Main()
{
    IBrandService service = new BrandService(repo, adapter);
    service.CreateBrand(dto); // ← đơn giản, không cần biết bên trong làm gì
}
```

### Interface Service

```csharp
// Services/InterfaceServices/IBrandService.cs
public interface IBrandService
{
    void CreateBrand(BrandDTO dto);     // Tạo brand mới
    void UpdateBrand(BrandDTO dto);     // Cập nhật brand
    List<Mbrand> GetAll();              // Lấy danh sách
}
```

### Hiện thực Service

```csharp
// Services/Services/BrandService.cs
public class BrandService : IBrandService
{
    private readonly IBrandRepository _repository; // ← inject Repository
    private readonly IBrandAdapter _adapter;        // ← inject Adapter

    public BrandService(IBrandRepository repository, IBrandAdapter adapter)
    {
        _repository = repository;
        _adapter = adapter;
    }

    public void CreateBrand(BrandDTO dto)
    {
        // 1. Dùng Adapter để chuyển DTO → Entity
        var entity = _adapter.ConvertToEntity(dto);
        // 2. Dùng Repository để lưu Entity
        _repository.Add(entity);
    }

    public void UpdateBrand(BrandDTO dto)
    {
        // Business logic: kiểm tra tồn tại trước khi update
        var existing = _repository.GetById(dto.BrandId);
        if (existing == null)
        {
            Console.WriteLine("Brand not found!");
            return;
        }

        // Chỉ cập nhật các field được phép thay đổi
        existing.BrandName   = dto.BrandName;
        existing.Description = dto.Description;
        existing.UpdatedAt   = DateTime.Now; // ← auto-update timestamp

        _repository.Update(existing);
    }

    public List<Mbrand> GetAll()
    {
        return _repository.GetAll();
    }
}
```

### Cách sử dụng trong Program.cs

```csharp
// DaoNguyenTrong.DesginPattern/Program.cs
IBrandRepository brandRepository = new BrandRepository();
IBrandAdapter    brandAdapter    = new BrandAdapter();
IBrandService    brandService    = new BrandService(brandRepository, brandAdapter);

// Tạo brand mới
brandService.CreateBrand(new BrandDTO
{
    BrandId   = "B001",
    BrandName = "Trek",
    Description = "American bicycle brand"
});

// Lấy danh sách
var brands = brandService.GetAll();
foreach (var b in brands)
    Console.WriteLine($"{b.BrandId} - {b.BrandName}");
```

### Khi nào dùng Service Layer?

| Tình huống | Có nên dùng? |
|---|---|
| Cần tập trung business logic vào một chỗ | ✅ Nên dùng |
| UI layer (Console/API/Web) cần được đơn giản hóa | ✅ Nên dùng |
| Cần dùng chung logic ở nhiều nơi | ✅ Nên dùng |
| Project quá nhỏ (< 3 entities) | ❌ Có thể không cần |

---

## 5. DTO Pattern

### Khái niệm

**DTO (Data Transfer Object) Pattern** là kỹ thuật dùng **object đơn giản chỉ để truyền dữ liệu** giữa các layer, tách biệt hoàn toàn với entity domain.

### Vấn đề giải quyết

Entity `Mbrand` chứa nhiều thứ mà UI không cần:
- Navigation properties: `ICollection<Mproduct> Mproducts`
- Metadata: `CreatedAt`, `UpdatedAt`, `IsActive`
- EF Core annotations

→ DTO chỉ chứa **đúng những gì UI cần**:

```csharp
// Domain/DTO/BrandDTO.cs - Chỉ có 3 field cần thiết cho UI
public class BrandDTO
{
    public string BrandId   { get; set; }
    public string BrandName { get; set; }
    public string Description { get; set; }
}

// Domain/DTO/OrderDTO.cs - Chỉ có dữ liệu user nhập
public class OrderDTO
{
    public string  OrderId         { get; set; }
    public string  BuyerId         { get; set; }
    public string  SellerId        { get; set; }
    public decimal TotalAmount     { get; set; }
    public string  ShippingAddress { get; set; }
    public string  ReceiverName    { get; set; }
    public string  ReceiverPhone   { get; set; }
    public int     DeliveryMethod  { get; set; }
    public string  Note            { get; set; }
    // Không có: OrderStatus, PaymentStatus, CreatedAt (do Adapter tự set)
}
```

### So sánh DTO vs Entity

| | DTO | Entity |
|---|---|---|
| **Mục đích** | Truyền dữ liệu | Ánh xạ với database |
| **EF Core annotations** | ❌ Không có | ✅ Có |
| **Navigation properties** | ❌ Không có | ✅ Có |
| **Metadata fields** | ❌ Không có | ✅ Có (CreatedAt, ...) |
| **Ai tạo** | UI/API | Adapter/Service |

---

## 6. Dependency Injection (Bonus)

### Khái niệm

**Dependency Injection (DI)** là kỹ thuật cung cấp các phụ thuộc (dependencies) cho object từ bên ngoài thay vì để object tự tạo chúng.

Dự án **LeQuocViet** sử dụng DI Container chính thức của .NET:

```csharp
// LeQuocViet/Program.cs
var services = new ServiceCollection();

// Đăng ký DbContext
services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("connection-string"));

// Đăng ký Repository
services.AddScoped<IMWishlistRepository, MWishlistRepository>();

// Đăng ký Service
services.AddScoped<IMWishlistService, MWishlistService>();

// Build DI Container
var serviceProvider = services.BuildServiceProvider();

// Lấy service (DI tự động inject các dependencies)
using var scope = serviceProvider.CreateScope();
var wishlistService = scope.ServiceProvider.GetRequiredService<IMWishlistService>();
```

### Không dùng DI vs Có dùng DI

```csharp
// ❌ Không dùng DI: hard-coded dependencies, khó test
var service = new BrandService(
    new BrandRepository(),   // ← tự tạo dependency
    new BrandAdapter()       // ← tự tạo dependency
);

// ✅ Dùng DI Container: dependencies được inject tự động
// Chỉ cần: var service = scope.ServiceProvider.GetRequiredService<IBrandService>();
```

---

## 7. Luồng xử lý tổng thể

Dưới đây là luồng đầy đủ khi tạo một **Brand** mới trong dự án:

```
1. NGƯỜI DÙNG nhập: BrandId="B001", BrandName="Trek", Description="..."
         ↓
2. PROGRAM.CS tạo BrandDTO và gọi:
   brandService.CreateBrand(dto)
         ↓
3. BRANDSERVICE nhận DTO, gọi adapter:
   var entity = _adapter.ConvertToEntity(dto)
         ↓
4. BRANDADAPTER nhận DTO, dùng Builder để tạo entity:
   _builder
     .SetId("B001")
     .SetName("Trek")
     .SetDescription("...")
     .SetIsActive(true)          ← Adapter tự set giá trị mặc định
     .SetCreatedAt(DateTime.Now) ← Adapter tự set thời gian
     .Build()
         ↓
5. BRANDBUILDER tạo và trả về đối tượng Mbrand hoàn chỉnh
         ↓
6. BRANDSERVICE nhận Mbrand entity, gọi repository:
   _repository.Add(entity)
         ↓
7. BRANDREPOSITORY lưu entity vào List<Mbrand> (in-memory)
   hoặc vào PostgreSQL (EF Core)
         ↓
8. HOÀN TẤT: Brand đã được lưu thành công!
```

### Sơ đồ phụ thuộc

```
Program.cs
    │
    ▼
IBrandService ──── BrandService
                       │
          ┌────────────┴────────────┐
          ▼                         ▼
  IBrandRepository           IBrandAdapter
  BrandRepository             BrandAdapter
  (lưu dữ liệu)                   │
                                   ▼
                             IBrandBuilder
                             BrandBuilder
                             (tạo entity)
```

---

## 8. Tóm tắt so sánh các Pattern

| Pattern | Loại | Mục đích chính | Vị trí trong dự án |
|---|---|---|---|
| **Builder** | Creational | Tạo object phức tạp từng bước | `BuilderPattern/` |
| **Adapter** | Structural | Chuyển đổi DTO ↔ Entity | `AdapterPattern/` |
| **Repository** | Architectural | Trừu tượng hóa data access | `Repositories/` |
| **Service/Facade** | Architectural | Đóng gói business logic | `Services/` |
| **DTO** | Architectural | Tách biệt data transfer với domain | `Domain/DTO/` |
| **DI** | Technique | Quản lý và inject dependencies | `LeQuocViet/` |

### Nguyên tắc thiết kế được áp dụng

- **Single Responsibility Principle (SRP):** Mỗi class có đúng một trách nhiệm
  - `BrandBuilder`: chỉ tạo `Mbrand`
  - `BrandAdapter`: chỉ chuyển đổi `BrandDTO` → `Mbrand`
  - `BrandRepository`: chỉ lưu/đọc `Mbrand`
  - `BrandService`: chỉ xử lý business logic về brand

- **Dependency Inversion Principle (DIP):** Phụ thuộc vào interface, không phụ thuộc vào implementation
  - `BrandService` phụ thuộc `IBrandRepository`, không phải `BrandRepository`
  - `BrandAdapter` phụ thuộc `IBrandBuilder`, không phải `BrandBuilder`

- **Open/Closed Principle (OCP):** Mở để mở rộng, đóng để sửa đổi
  - Có thể thêm `BrandEfRepository : IBrandRepository` mà không cần sửa `BrandService`

---

> 📚 **Tài liệu tham khảo thêm:**
> - [Refactoring.Guru – Builder Pattern](https://refactoring.guru/design-patterns/builder)
> - [Refactoring.Guru – Adapter Pattern](https://refactoring.guru/design-patterns/adapter)
> - [Martin Fowler – Repository Pattern](https://martinfowler.com/eaaCatalog/repository.html)
> - [Microsoft Docs – Dependency Injection in .NET](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
