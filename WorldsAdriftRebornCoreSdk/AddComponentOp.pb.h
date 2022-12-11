// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: AddComponentOp.proto

#ifndef GOOGLE_PROTOBUF_INCLUDED_AddComponentOp_2eproto
#define GOOGLE_PROTOBUF_INCLUDED_AddComponentOp_2eproto

#include <limits>
#include <string>

#include <google/protobuf/port_def.inc>
#if PROTOBUF_VERSION < 3021000
#error This file was generated by a newer version of protoc which is
#error incompatible with your Protocol Buffer headers. Please update
#error your headers.
#endif
#if 3021008 < PROTOBUF_MIN_PROTOC_VERSION
#error This file was generated by an older version of protoc which is
#error incompatible with your Protocol Buffer headers. Please
#error regenerate this file with a newer version of protoc.
#endif

#include <google/protobuf/port_undef.inc>
#include <google/protobuf/io/coded_stream.h>
#include <google/protobuf/arena.h>
#include <google/protobuf/arenastring.h>
#include <google/protobuf/generated_message_util.h>
#include <google/protobuf/metadata_lite.h>
#include <google/protobuf/generated_message_reflection.h>
#include <google/protobuf/message.h>
#include <google/protobuf/repeated_field.h>  // IWYU pragma: export
#include <google/protobuf/extension_set.h>  // IWYU pragma: export
#include <google/protobuf/unknown_field_set.h>
// @@protoc_insertion_point(includes)
#include <google/protobuf/port_def.inc>
#define PROTOBUF_INTERNAL_EXPORT_AddComponentOp_2eproto
PROTOBUF_NAMESPACE_OPEN
namespace internal {
class AnyMetadata;
}  // namespace internal
PROTOBUF_NAMESPACE_CLOSE

// Internal implementation detail -- do not use these members.
struct TableStruct_AddComponentOp_2eproto {
  static const uint32_t offsets[];
};
extern const ::PROTOBUF_NAMESPACE_ID::internal::DescriptorTable descriptor_table_AddComponentOp_2eproto;
namespace WorldsAdriftRebornCoreSdk {
class AddComponentOp;
struct AddComponentOpDefaultTypeInternal;
extern AddComponentOpDefaultTypeInternal _AddComponentOp_default_instance_;
class ComponentData;
struct ComponentDataDefaultTypeInternal;
extern ComponentDataDefaultTypeInternal _ComponentData_default_instance_;
}  // namespace WorldsAdriftRebornCoreSdk
PROTOBUF_NAMESPACE_OPEN
template<> ::WorldsAdriftRebornCoreSdk::AddComponentOp* Arena::CreateMaybeMessage<::WorldsAdriftRebornCoreSdk::AddComponentOp>(Arena*);
template<> ::WorldsAdriftRebornCoreSdk::ComponentData* Arena::CreateMaybeMessage<::WorldsAdriftRebornCoreSdk::ComponentData>(Arena*);
PROTOBUF_NAMESPACE_CLOSE
namespace WorldsAdriftRebornCoreSdk {

// ===================================================================

class AddComponentOp final :
    public ::PROTOBUF_NAMESPACE_ID::Message /* @@protoc_insertion_point(class_definition:WorldsAdriftRebornCoreSdk.AddComponentOp) */ {
 public:
  inline AddComponentOp() : AddComponentOp(nullptr) {}
  ~AddComponentOp() override;
  explicit PROTOBUF_CONSTEXPR AddComponentOp(::PROTOBUF_NAMESPACE_ID::internal::ConstantInitialized);

  AddComponentOp(const AddComponentOp& from);
  AddComponentOp(AddComponentOp&& from) noexcept
    : AddComponentOp() {
    *this = ::std::move(from);
  }

  inline AddComponentOp& operator=(const AddComponentOp& from) {
    CopyFrom(from);
    return *this;
  }
  inline AddComponentOp& operator=(AddComponentOp&& from) noexcept {
    if (this == &from) return *this;
    if (GetOwningArena() == from.GetOwningArena()
  #ifdef PROTOBUF_FORCE_COPY_IN_MOVE
        && GetOwningArena() != nullptr
  #endif  // !PROTOBUF_FORCE_COPY_IN_MOVE
    ) {
      InternalSwap(&from);
    } else {
      CopyFrom(from);
    }
    return *this;
  }

  static const ::PROTOBUF_NAMESPACE_ID::Descriptor* descriptor() {
    return GetDescriptor();
  }
  static const ::PROTOBUF_NAMESPACE_ID::Descriptor* GetDescriptor() {
    return default_instance().GetMetadata().descriptor;
  }
  static const ::PROTOBUF_NAMESPACE_ID::Reflection* GetReflection() {
    return default_instance().GetMetadata().reflection;
  }
  static const AddComponentOp& default_instance() {
    return *internal_default_instance();
  }
  static inline const AddComponentOp* internal_default_instance() {
    return reinterpret_cast<const AddComponentOp*>(
               &_AddComponentOp_default_instance_);
  }
  static constexpr int kIndexInFileMessages =
    0;

  friend void swap(AddComponentOp& a, AddComponentOp& b) {
    a.Swap(&b);
  }
  inline void Swap(AddComponentOp* other) {
    if (other == this) return;
  #ifdef PROTOBUF_FORCE_COPY_IN_SWAP
    if (GetOwningArena() != nullptr &&
        GetOwningArena() == other->GetOwningArena()) {
   #else  // PROTOBUF_FORCE_COPY_IN_SWAP
    if (GetOwningArena() == other->GetOwningArena()) {
  #endif  // !PROTOBUF_FORCE_COPY_IN_SWAP
      InternalSwap(other);
    } else {
      ::PROTOBUF_NAMESPACE_ID::internal::GenericSwap(this, other);
    }
  }
  void UnsafeArenaSwap(AddComponentOp* other) {
    if (other == this) return;
    GOOGLE_DCHECK(GetOwningArena() == other->GetOwningArena());
    InternalSwap(other);
  }

  // implements Message ----------------------------------------------

  AddComponentOp* New(::PROTOBUF_NAMESPACE_ID::Arena* arena = nullptr) const final {
    return CreateMaybeMessage<AddComponentOp>(arena);
  }
  using ::PROTOBUF_NAMESPACE_ID::Message::CopyFrom;
  void CopyFrom(const AddComponentOp& from);
  using ::PROTOBUF_NAMESPACE_ID::Message::MergeFrom;
  void MergeFrom( const AddComponentOp& from) {
    AddComponentOp::MergeImpl(*this, from);
  }
  private:
  static void MergeImpl(::PROTOBUF_NAMESPACE_ID::Message& to_msg, const ::PROTOBUF_NAMESPACE_ID::Message& from_msg);
  public:
  PROTOBUF_ATTRIBUTE_REINITIALIZES void Clear() final;
  bool IsInitialized() const final;

  size_t ByteSizeLong() const final;
  const char* _InternalParse(const char* ptr, ::PROTOBUF_NAMESPACE_ID::internal::ParseContext* ctx) final;
  uint8_t* _InternalSerialize(
      uint8_t* target, ::PROTOBUF_NAMESPACE_ID::io::EpsCopyOutputStream* stream) const final;
  int GetCachedSize() const final { return _impl_._cached_size_.Get(); }

  private:
  void SharedCtor(::PROTOBUF_NAMESPACE_ID::Arena* arena, bool is_message_owned);
  void SharedDtor();
  void SetCachedSize(int size) const final;
  void InternalSwap(AddComponentOp* other);

  private:
  friend class ::PROTOBUF_NAMESPACE_ID::internal::AnyMetadata;
  static ::PROTOBUF_NAMESPACE_ID::StringPiece FullMessageName() {
    return "WorldsAdriftRebornCoreSdk.AddComponentOp";
  }
  protected:
  explicit AddComponentOp(::PROTOBUF_NAMESPACE_ID::Arena* arena,
                       bool is_message_owned = false);
  public:

  static const ClassData _class_data_;
  const ::PROTOBUF_NAMESPACE_ID::Message::ClassData*GetClassData() const final;

  ::PROTOBUF_NAMESPACE_ID::Metadata GetMetadata() const final;

  // nested types ----------------------------------------------------

  // accessors -------------------------------------------------------

  enum : int {
    kComponentsFieldNumber = 2,
    kEntityIdFieldNumber = 1,
  };
  // repeated .WorldsAdriftRebornCoreSdk.ComponentData Components = 2;
  int components_size() const;
  private:
  int _internal_components_size() const;
  public:
  void clear_components();
  ::WorldsAdriftRebornCoreSdk::ComponentData* mutable_components(int index);
  ::PROTOBUF_NAMESPACE_ID::RepeatedPtrField< ::WorldsAdriftRebornCoreSdk::ComponentData >*
      mutable_components();
  private:
  const ::WorldsAdriftRebornCoreSdk::ComponentData& _internal_components(int index) const;
  ::WorldsAdriftRebornCoreSdk::ComponentData* _internal_add_components();
  public:
  const ::WorldsAdriftRebornCoreSdk::ComponentData& components(int index) const;
  ::WorldsAdriftRebornCoreSdk::ComponentData* add_components();
  const ::PROTOBUF_NAMESPACE_ID::RepeatedPtrField< ::WorldsAdriftRebornCoreSdk::ComponentData >&
      components() const;

  // optional int64 EntityId = 1;
  bool has_entityid() const;
  private:
  bool _internal_has_entityid() const;
  public:
  void clear_entityid();
  int64_t entityid() const;
  void set_entityid(int64_t value);
  private:
  int64_t _internal_entityid() const;
  void _internal_set_entityid(int64_t value);
  public:

  // @@protoc_insertion_point(class_scope:WorldsAdriftRebornCoreSdk.AddComponentOp)
 private:
  class _Internal;

  template <typename T> friend class ::PROTOBUF_NAMESPACE_ID::Arena::InternalHelper;
  typedef void InternalArenaConstructable_;
  typedef void DestructorSkippable_;
  struct Impl_ {
    ::PROTOBUF_NAMESPACE_ID::internal::HasBits<1> _has_bits_;
    mutable ::PROTOBUF_NAMESPACE_ID::internal::CachedSize _cached_size_;
    ::PROTOBUF_NAMESPACE_ID::RepeatedPtrField< ::WorldsAdriftRebornCoreSdk::ComponentData > components_;
    int64_t entityid_;
  };
  union { Impl_ _impl_; };
  friend struct ::TableStruct_AddComponentOp_2eproto;
};
// -------------------------------------------------------------------

class ComponentData final :
    public ::PROTOBUF_NAMESPACE_ID::Message /* @@protoc_insertion_point(class_definition:WorldsAdriftRebornCoreSdk.ComponentData) */ {
 public:
  inline ComponentData() : ComponentData(nullptr) {}
  ~ComponentData() override;
  explicit PROTOBUF_CONSTEXPR ComponentData(::PROTOBUF_NAMESPACE_ID::internal::ConstantInitialized);

  ComponentData(const ComponentData& from);
  ComponentData(ComponentData&& from) noexcept
    : ComponentData() {
    *this = ::std::move(from);
  }

  inline ComponentData& operator=(const ComponentData& from) {
    CopyFrom(from);
    return *this;
  }
  inline ComponentData& operator=(ComponentData&& from) noexcept {
    if (this == &from) return *this;
    if (GetOwningArena() == from.GetOwningArena()
  #ifdef PROTOBUF_FORCE_COPY_IN_MOVE
        && GetOwningArena() != nullptr
  #endif  // !PROTOBUF_FORCE_COPY_IN_MOVE
    ) {
      InternalSwap(&from);
    } else {
      CopyFrom(from);
    }
    return *this;
  }

  static const ::PROTOBUF_NAMESPACE_ID::Descriptor* descriptor() {
    return GetDescriptor();
  }
  static const ::PROTOBUF_NAMESPACE_ID::Descriptor* GetDescriptor() {
    return default_instance().GetMetadata().descriptor;
  }
  static const ::PROTOBUF_NAMESPACE_ID::Reflection* GetReflection() {
    return default_instance().GetMetadata().reflection;
  }
  static const ComponentData& default_instance() {
    return *internal_default_instance();
  }
  static inline const ComponentData* internal_default_instance() {
    return reinterpret_cast<const ComponentData*>(
               &_ComponentData_default_instance_);
  }
  static constexpr int kIndexInFileMessages =
    1;

  friend void swap(ComponentData& a, ComponentData& b) {
    a.Swap(&b);
  }
  inline void Swap(ComponentData* other) {
    if (other == this) return;
  #ifdef PROTOBUF_FORCE_COPY_IN_SWAP
    if (GetOwningArena() != nullptr &&
        GetOwningArena() == other->GetOwningArena()) {
   #else  // PROTOBUF_FORCE_COPY_IN_SWAP
    if (GetOwningArena() == other->GetOwningArena()) {
  #endif  // !PROTOBUF_FORCE_COPY_IN_SWAP
      InternalSwap(other);
    } else {
      ::PROTOBUF_NAMESPACE_ID::internal::GenericSwap(this, other);
    }
  }
  void UnsafeArenaSwap(ComponentData* other) {
    if (other == this) return;
    GOOGLE_DCHECK(GetOwningArena() == other->GetOwningArena());
    InternalSwap(other);
  }

  // implements Message ----------------------------------------------

  ComponentData* New(::PROTOBUF_NAMESPACE_ID::Arena* arena = nullptr) const final {
    return CreateMaybeMessage<ComponentData>(arena);
  }
  using ::PROTOBUF_NAMESPACE_ID::Message::CopyFrom;
  void CopyFrom(const ComponentData& from);
  using ::PROTOBUF_NAMESPACE_ID::Message::MergeFrom;
  void MergeFrom( const ComponentData& from) {
    ComponentData::MergeImpl(*this, from);
  }
  private:
  static void MergeImpl(::PROTOBUF_NAMESPACE_ID::Message& to_msg, const ::PROTOBUF_NAMESPACE_ID::Message& from_msg);
  public:
  PROTOBUF_ATTRIBUTE_REINITIALIZES void Clear() final;
  bool IsInitialized() const final;

  size_t ByteSizeLong() const final;
  const char* _InternalParse(const char* ptr, ::PROTOBUF_NAMESPACE_ID::internal::ParseContext* ctx) final;
  uint8_t* _InternalSerialize(
      uint8_t* target, ::PROTOBUF_NAMESPACE_ID::io::EpsCopyOutputStream* stream) const final;
  int GetCachedSize() const final { return _impl_._cached_size_.Get(); }

  private:
  void SharedCtor(::PROTOBUF_NAMESPACE_ID::Arena* arena, bool is_message_owned);
  void SharedDtor();
  void SetCachedSize(int size) const final;
  void InternalSwap(ComponentData* other);

  private:
  friend class ::PROTOBUF_NAMESPACE_ID::internal::AnyMetadata;
  static ::PROTOBUF_NAMESPACE_ID::StringPiece FullMessageName() {
    return "WorldsAdriftRebornCoreSdk.ComponentData";
  }
  protected:
  explicit ComponentData(::PROTOBUF_NAMESPACE_ID::Arena* arena,
                       bool is_message_owned = false);
  public:

  static const ClassData _class_data_;
  const ::PROTOBUF_NAMESPACE_ID::Message::ClassData*GetClassData() const final;

  ::PROTOBUF_NAMESPACE_ID::Metadata GetMetadata() const final;

  // nested types ----------------------------------------------------

  // accessors -------------------------------------------------------

  enum : int {
    kDataFieldNumber = 2,
    kComponentIdFieldNumber = 1,
    kDataLengthFieldNumber = 3,
  };
  // optional bytes Data = 2;
  bool has_data() const;
  private:
  bool _internal_has_data() const;
  public:
  void clear_data();
  const std::string& data() const;
  template <typename ArgT0 = const std::string&, typename... ArgT>
  void set_data(ArgT0&& arg0, ArgT... args);
  std::string* mutable_data();
  PROTOBUF_NODISCARD std::string* release_data();
  void set_allocated_data(std::string* data);
  private:
  const std::string& _internal_data() const;
  inline PROTOBUF_ALWAYS_INLINE void _internal_set_data(const std::string& value);
  std::string* _internal_mutable_data();
  public:

  // optional uint32 ComponentId = 1;
  bool has_componentid() const;
  private:
  bool _internal_has_componentid() const;
  public:
  void clear_componentid();
  uint32_t componentid() const;
  void set_componentid(uint32_t value);
  private:
  uint32_t _internal_componentid() const;
  void _internal_set_componentid(uint32_t value);
  public:

  // optional int32 DataLength = 3;
  bool has_datalength() const;
  private:
  bool _internal_has_datalength() const;
  public:
  void clear_datalength();
  int32_t datalength() const;
  void set_datalength(int32_t value);
  private:
  int32_t _internal_datalength() const;
  void _internal_set_datalength(int32_t value);
  public:

  // @@protoc_insertion_point(class_scope:WorldsAdriftRebornCoreSdk.ComponentData)
 private:
  class _Internal;

  template <typename T> friend class ::PROTOBUF_NAMESPACE_ID::Arena::InternalHelper;
  typedef void InternalArenaConstructable_;
  typedef void DestructorSkippable_;
  struct Impl_ {
    ::PROTOBUF_NAMESPACE_ID::internal::HasBits<1> _has_bits_;
    mutable ::PROTOBUF_NAMESPACE_ID::internal::CachedSize _cached_size_;
    ::PROTOBUF_NAMESPACE_ID::internal::ArenaStringPtr data_;
    uint32_t componentid_;
    int32_t datalength_;
  };
  union { Impl_ _impl_; };
  friend struct ::TableStruct_AddComponentOp_2eproto;
};
// ===================================================================


// ===================================================================

#ifdef __GNUC__
  #pragma GCC diagnostic push
  #pragma GCC diagnostic ignored "-Wstrict-aliasing"
#endif  // __GNUC__
// AddComponentOp

// optional int64 EntityId = 1;
inline bool AddComponentOp::_internal_has_entityid() const {
  bool value = (_impl_._has_bits_[0] & 0x00000001u) != 0;
  return value;
}
inline bool AddComponentOp::has_entityid() const {
  return _internal_has_entityid();
}
inline void AddComponentOp::clear_entityid() {
  _impl_.entityid_ = int64_t{0};
  _impl_._has_bits_[0] &= ~0x00000001u;
}
inline int64_t AddComponentOp::_internal_entityid() const {
  return _impl_.entityid_;
}
inline int64_t AddComponentOp::entityid() const {
  // @@protoc_insertion_point(field_get:WorldsAdriftRebornCoreSdk.AddComponentOp.EntityId)
  return _internal_entityid();
}
inline void AddComponentOp::_internal_set_entityid(int64_t value) {
  _impl_._has_bits_[0] |= 0x00000001u;
  _impl_.entityid_ = value;
}
inline void AddComponentOp::set_entityid(int64_t value) {
  _internal_set_entityid(value);
  // @@protoc_insertion_point(field_set:WorldsAdriftRebornCoreSdk.AddComponentOp.EntityId)
}

// repeated .WorldsAdriftRebornCoreSdk.ComponentData Components = 2;
inline int AddComponentOp::_internal_components_size() const {
  return _impl_.components_.size();
}
inline int AddComponentOp::components_size() const {
  return _internal_components_size();
}
inline void AddComponentOp::clear_components() {
  _impl_.components_.Clear();
}
inline ::WorldsAdriftRebornCoreSdk::ComponentData* AddComponentOp::mutable_components(int index) {
  // @@protoc_insertion_point(field_mutable:WorldsAdriftRebornCoreSdk.AddComponentOp.Components)
  return _impl_.components_.Mutable(index);
}
inline ::PROTOBUF_NAMESPACE_ID::RepeatedPtrField< ::WorldsAdriftRebornCoreSdk::ComponentData >*
AddComponentOp::mutable_components() {
  // @@protoc_insertion_point(field_mutable_list:WorldsAdriftRebornCoreSdk.AddComponentOp.Components)
  return &_impl_.components_;
}
inline const ::WorldsAdriftRebornCoreSdk::ComponentData& AddComponentOp::_internal_components(int index) const {
  return _impl_.components_.Get(index);
}
inline const ::WorldsAdriftRebornCoreSdk::ComponentData& AddComponentOp::components(int index) const {
  // @@protoc_insertion_point(field_get:WorldsAdriftRebornCoreSdk.AddComponentOp.Components)
  return _internal_components(index);
}
inline ::WorldsAdriftRebornCoreSdk::ComponentData* AddComponentOp::_internal_add_components() {
  return _impl_.components_.Add();
}
inline ::WorldsAdriftRebornCoreSdk::ComponentData* AddComponentOp::add_components() {
  ::WorldsAdriftRebornCoreSdk::ComponentData* _add = _internal_add_components();
  // @@protoc_insertion_point(field_add:WorldsAdriftRebornCoreSdk.AddComponentOp.Components)
  return _add;
}
inline const ::PROTOBUF_NAMESPACE_ID::RepeatedPtrField< ::WorldsAdriftRebornCoreSdk::ComponentData >&
AddComponentOp::components() const {
  // @@protoc_insertion_point(field_list:WorldsAdriftRebornCoreSdk.AddComponentOp.Components)
  return _impl_.components_;
}

// -------------------------------------------------------------------

// ComponentData

// optional uint32 ComponentId = 1;
inline bool ComponentData::_internal_has_componentid() const {
  bool value = (_impl_._has_bits_[0] & 0x00000002u) != 0;
  return value;
}
inline bool ComponentData::has_componentid() const {
  return _internal_has_componentid();
}
inline void ComponentData::clear_componentid() {
  _impl_.componentid_ = 0u;
  _impl_._has_bits_[0] &= ~0x00000002u;
}
inline uint32_t ComponentData::_internal_componentid() const {
  return _impl_.componentid_;
}
inline uint32_t ComponentData::componentid() const {
  // @@protoc_insertion_point(field_get:WorldsAdriftRebornCoreSdk.ComponentData.ComponentId)
  return _internal_componentid();
}
inline void ComponentData::_internal_set_componentid(uint32_t value) {
  _impl_._has_bits_[0] |= 0x00000002u;
  _impl_.componentid_ = value;
}
inline void ComponentData::set_componentid(uint32_t value) {
  _internal_set_componentid(value);
  // @@protoc_insertion_point(field_set:WorldsAdriftRebornCoreSdk.ComponentData.ComponentId)
}

// optional bytes Data = 2;
inline bool ComponentData::_internal_has_data() const {
  bool value = (_impl_._has_bits_[0] & 0x00000001u) != 0;
  return value;
}
inline bool ComponentData::has_data() const {
  return _internal_has_data();
}
inline void ComponentData::clear_data() {
  _impl_.data_.ClearToEmpty();
  _impl_._has_bits_[0] &= ~0x00000001u;
}
inline const std::string& ComponentData::data() const {
  // @@protoc_insertion_point(field_get:WorldsAdriftRebornCoreSdk.ComponentData.Data)
  return _internal_data();
}
template <typename ArgT0, typename... ArgT>
inline PROTOBUF_ALWAYS_INLINE
void ComponentData::set_data(ArgT0&& arg0, ArgT... args) {
 _impl_._has_bits_[0] |= 0x00000001u;
 _impl_.data_.SetBytes(static_cast<ArgT0 &&>(arg0), args..., GetArenaForAllocation());
  // @@protoc_insertion_point(field_set:WorldsAdriftRebornCoreSdk.ComponentData.Data)
}
inline std::string* ComponentData::mutable_data() {
  std::string* _s = _internal_mutable_data();
  // @@protoc_insertion_point(field_mutable:WorldsAdriftRebornCoreSdk.ComponentData.Data)
  return _s;
}
inline const std::string& ComponentData::_internal_data() const {
  return _impl_.data_.Get();
}
inline void ComponentData::_internal_set_data(const std::string& value) {
  _impl_._has_bits_[0] |= 0x00000001u;
  _impl_.data_.Set(value, GetArenaForAllocation());
}
inline std::string* ComponentData::_internal_mutable_data() {
  _impl_._has_bits_[0] |= 0x00000001u;
  return _impl_.data_.Mutable(GetArenaForAllocation());
}
inline std::string* ComponentData::release_data() {
  // @@protoc_insertion_point(field_release:WorldsAdriftRebornCoreSdk.ComponentData.Data)
  if (!_internal_has_data()) {
    return nullptr;
  }
  _impl_._has_bits_[0] &= ~0x00000001u;
  auto* p = _impl_.data_.Release();
#ifdef PROTOBUF_FORCE_COPY_DEFAULT_STRING
  if (_impl_.data_.IsDefault()) {
    _impl_.data_.Set("", GetArenaForAllocation());
  }
#endif // PROTOBUF_FORCE_COPY_DEFAULT_STRING
  return p;
}
inline void ComponentData::set_allocated_data(std::string* data) {
  if (data != nullptr) {
    _impl_._has_bits_[0] |= 0x00000001u;
  } else {
    _impl_._has_bits_[0] &= ~0x00000001u;
  }
  _impl_.data_.SetAllocated(data, GetArenaForAllocation());
#ifdef PROTOBUF_FORCE_COPY_DEFAULT_STRING
  if (_impl_.data_.IsDefault()) {
    _impl_.data_.Set("", GetArenaForAllocation());
  }
#endif // PROTOBUF_FORCE_COPY_DEFAULT_STRING
  // @@protoc_insertion_point(field_set_allocated:WorldsAdriftRebornCoreSdk.ComponentData.Data)
}

// optional int32 DataLength = 3;
inline bool ComponentData::_internal_has_datalength() const {
  bool value = (_impl_._has_bits_[0] & 0x00000004u) != 0;
  return value;
}
inline bool ComponentData::has_datalength() const {
  return _internal_has_datalength();
}
inline void ComponentData::clear_datalength() {
  _impl_.datalength_ = 0;
  _impl_._has_bits_[0] &= ~0x00000004u;
}
inline int32_t ComponentData::_internal_datalength() const {
  return _impl_.datalength_;
}
inline int32_t ComponentData::datalength() const {
  // @@protoc_insertion_point(field_get:WorldsAdriftRebornCoreSdk.ComponentData.DataLength)
  return _internal_datalength();
}
inline void ComponentData::_internal_set_datalength(int32_t value) {
  _impl_._has_bits_[0] |= 0x00000004u;
  _impl_.datalength_ = value;
}
inline void ComponentData::set_datalength(int32_t value) {
  _internal_set_datalength(value);
  // @@protoc_insertion_point(field_set:WorldsAdriftRebornCoreSdk.ComponentData.DataLength)
}

#ifdef __GNUC__
  #pragma GCC diagnostic pop
#endif  // __GNUC__
// -------------------------------------------------------------------


// @@protoc_insertion_point(namespace_scope)

}  // namespace WorldsAdriftRebornCoreSdk

// @@protoc_insertion_point(global_scope)

#include <google/protobuf/port_undef.inc>
#endif  // GOOGLE_PROTOBUF_INCLUDED_GOOGLE_PROTOBUF_INCLUDED_AddComponentOp_2eproto
