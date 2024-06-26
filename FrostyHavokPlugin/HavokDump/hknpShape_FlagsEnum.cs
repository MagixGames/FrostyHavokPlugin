using System;
namespace hk;
[Flags]
public enum hknpShape_FlagsEnum : uint
{
    IS_CONVEX_SHAPE = 1,
    IS_CONVEX_POLYTOPE_SHAPE = 2,
    IS_COMPOSITE_SHAPE = 4,
    IS_HEIGHT_FIELD_SHAPE = 8,
    USE_SINGLE_POINT_MANIFOLD = 16,
    IS_TRIANGLE_OR_QUAD_NO_EDGES = 32,
    SUPPORTS_BPLANE_COLLISIONS = 64,
    USE_NORMAL_TO_FIND_SUPPORT_PLANE = 128,
    USE_SMALL_FACE_INDICES = 256,
    NO_GET_ALL_SHAPE_KEYS_ON_SPU = 512,
    SHAPE_NOT_SUPPORTED_ON_SPU = 1024,
    CONTAINS_ONLY_TRIANGLES = 2048,
    UNK1_NEG_SCALE_X = 4096,
}
