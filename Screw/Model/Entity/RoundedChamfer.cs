using Kompas6API5;
using Kompas6Constants3D;
using Screw.Model.Point;
using Screw.Model.FigureParam;
using Screw.Model.Entity;
using Screw.Error;
using Screw.Validator;

namespace Screw.Model.Entity
{
    /// <summary>
    /// Rounded chamfer
    /// </summary>
    class RoundedChamfer
    {
        ///// <summary>
        ///// Kompas application specimen
        ///// </summary>
        //private KompasApplication _kompasApp;

        ///// <summary>
        ///// Struct with parameters of rounded chamfer
        ///// </summary>
        //private RoundedChamferParameters _figureParameters;

        ///// <summary>
        ///// Last error code getter
        ///// </summary>
        //public ErrorCodes LastErrorCode
        //{
        //    get;
        //    private set;
        //}

        ///// <summary>
        ///// Rounded chamfer entity getter
        ///// </summary>
        //public ksEntity Entity
        //{
        //    get;
        //    private set;
        //}

        ///// <summary>
        ///// Create rounded chamfer in regular polygon sketch by base sketch, 
        ///// regular polygon parameters and base plane coordinates in plane
        ///// </summary>
        ///// <param name="doc3D">Kompas document 3D</param>
        ///// <param name="doc3DPart">Kompas document 3D part with detail</param>
        ///// <param name="regPolySketch">Sketch of regular polygon</param>
        ///// <param name="regPolyParam">An object with parameters of regular polygon</param>
        ///// <param name="basePlanePoint">Base plane point of regular polygon</param>
        ///// <param name="directionType">Direction type</param>
        ///// <returns>Entity of rounded chamfer or null in case of error</returns>
        //public RoundedChamfer(KompasApplication kompasApp, RoundedChamferParameters figureParameters)
        //{
        //    if (kompasApp == null
        //        || figureParameters.Document3DPart == null
        //        || figureParameters.RegularPolygonSketch == null
        //        || figureParameters.RegularPolygonParameters == null
        //    )
        //    {
        //        LastErrorCode = ErrorCodes.ArgumentNull;
        //        return;
        //    }

        //    // KompasApplication kompasApp, ksPart doc3DPart, ksEntity regPolySketch, RegularPolygonParameter regPolyParam, KompasPoint2D basePlanePoint, Direction_Type directionType

        //    _figureParameters = figureParameters;

        //    _kompasApp = kompasApp;

        //    if (!DoubleValidator.Validate(_figureParameters.BasePlanePoint.X)
        //        || !DoubleValidator.Validate(_figureParameters.BasePlanePoint.Y)
        //    )
        //    {
        //        LastErrorCode = ErrorCodes.DoubleValueValidationError;
        //        return;
        //    }
        //}

        ///// <summary>
        ///// Create rounded chamfer
        ///// </summary>
        ///// <returns>true if operation successful; false in case of error</returns>
        //public bool CreateDetail()
        //{
        //    // Rounded chamfer:
        //    // 1.1 - 1.2 Base part of rounded chamfer
        //    var baseOfChamfer = CreateBase(_figureParameters);
        //    if (baseOfChamfer == null)
        //    {
        //        return false;
        //    }

        //    // 1.3 - Section operation
        //    if (!CreateSection(_figureParameters, baseOfChamfer))
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        ///// <summary>
        ///// Create base of rounded chamfer
        ///// </summary>
        ///// <param name="figureParameters">Parameters of rounded chamfer</param>
        ///// <returns>Extruded entity of base of rounded chamfer</returns>
        //private ksEntity CreateBase(RoundedChamferParameters figureParameters)
        //{
        //    // 1.1 Base of rounded chamfer
        //    var innerCircleSketch = new KompasSketch(figureParameters.Document3DPart, figureParameters.RegularPolygonSketch);
        //    if (innerCircleSketch.LastErrorCode != ErrorCodes.OK)
        //    {
        //        LastErrorCode = innerCircleSketch.LastErrorCode;
        //        return null;
        //    }

        //    var innerCircleEdit = innerCircleSketch.BeginEntityEdit();
        //    if (innerCircleEdit == null)
        //    {
        //        LastErrorCode = innerCircleSketch.LastErrorCode;
        //        return null;
        //    }

        //    if (innerCircleEdit.ksCircle(figureParameters.BasePlanePoint.X, figureParameters.BasePlanePoint.Y, _kompasApp.Parameters[1] / 2.0, 1) == 0) // d
        //    {
        //        LastErrorCode = ErrorCodes.Document2DCircleCreatingError;
        //        return null;
        //    }

        //    innerCircleSketch.EndEntityEdit();

        //    // 1.2 Hat rounded chamfer base extrusion
        //    var extrusionParameters = new KompasExtrusionParameters(figureParameters.Document3DPart, Obj3dType.o3d_baseExtrusion, innerCircleSketch.Entity, figureParameters.Direction, _kompasApp.Parameters[4] * 0.16);
        //    var innerCircleExtrusion = new KompasExtrusion(extrusionParameters, ExtrusionType.ByEntity); // Height of chamfer is 0.16 * H
        //    if (innerCircleExtrusion.LastErrorCode != ErrorCodes.OK)
        //    {
        //        LastErrorCode = innerCircleExtrusion.LastErrorCode;
        //        return null;
        //    }

        //    innerCircleExtrusion.BaseFaceAreaState = KompasFaces.BaseFaceAreaState.BaseFaceAreaLower;
        //    var extruded = innerCircleExtrusion.ExtrudedEntity;
        //    if (extruded == null)
        //    {
        //        LastErrorCode = innerCircleExtrusion.LastErrorCode;
        //        return null;
        //    }

        //    return extruded;
        //}

        ///// <summary>
        ///// Create section operation of rounded chamfer
        ///// </summary>
        ///// <returns>true if operation successful; false in case of error</returns>
        //private bool CreateSection(RoundedChamferParameters figureParameters, ksEntity baseOfChamfer)
        //{
        //    // 1.3 Hat rounded chamfer sketches for section operation:
        //    // 1.3.1 Extra inner circle
        //    var extraInnerCircleSketch = new KompasSketch(figureParameters.Document3DPart, baseOfChamfer);
        //    if (extraInnerCircleSketch.LastErrorCode != ErrorCodes.OK)
        //    {
        //        LastErrorCode = extraInnerCircleSketch.LastErrorCode;
        //        return false;
        //    }

        //    var extraInnerCircleEdit = extraInnerCircleSketch.BeginEntityEdit();
        //    if (extraInnerCircleEdit == null)
        //    {
        //        LastErrorCode = extraInnerCircleSketch.LastErrorCode;
        //        return false;
        //    }

        //    if (extraInnerCircleEdit.ksCircle(figureParameters.BasePlanePoint.X, figureParameters.BasePlanePoint.Y, _kompasApp.Parameters[1] / 2.0, 1) == 0) // d
        //    {
        //        LastErrorCode = extraInnerCircleSketch.LastErrorCode;
        //        return false;
        //    }

        //    extraInnerCircleSketch.EndEntityEdit();

        //    // 1.3.3.2 Extra regular polygon
        //    var extraRegPolySketch = new KompasSketch(figureParameters.Document3DPart, figureParameters.RegularPolygonSketch);
        //    if (extraRegPolySketch.LastErrorCode != ErrorCodes.OK)
        //    {
        //        LastErrorCode = extraRegPolySketch.LastErrorCode;
        //        return false;
        //    }

        //    var extraRegPolyEdit = extraRegPolySketch.BeginEntityEdit();
        //    if (extraRegPolyEdit == null)
        //    {
        //        LastErrorCode = extraRegPolySketch.LastErrorCode;
        //        return false;
        //    }

        //    if (extraRegPolyEdit.ksRegularPolygon(figureParameters.RegularPolygonParameters.FigureParam, 0) == 0)
        //    {
        //        LastErrorCode = extraRegPolySketch.LastErrorCode;
        //        return false;
        //    }

        //    extraRegPolySketch.EndEntityEdit();

        //    // 1.3.4 Hat rounded chamfer section operation
        //    var screwChamferSketches = (ksEntityCollection)_kompasApp.Document3D.EntityCollection((short)Obj3dType.o3d_sketch);
        //    screwChamferSketches.Clear();
        //    screwChamferSketches.Add(extraInnerCircleSketch.Entity);
        //    screwChamferSketches.Add(extraRegPolySketch.Entity);
        //    screwChamferSketches.refresh();

        //    if (screwChamferSketches == null)
        //    {
        //        LastErrorCode = ErrorCodes.ArgumentNull;
        //        return false;
        //    }

        //    var extrusionParameters = new KompasExtrusionParameters(figureParameters.Document3DPart, Obj3dType.o3d_baseLoft, null, screwChamferSketches);
        //    var screwChamferExtrusion = new KompasExtrusion(extrusionParameters, ExtrusionType.BySketchesCollection);
        //    if (screwChamferExtrusion.LastErrorCode != ErrorCodes.OK)
        //    {
        //        LastErrorCode = screwChamferExtrusion.LastErrorCode;
        //        return false;
        //    }

        //    if (extraInnerCircleSketch.Entity == null)
        //    {
        //        LastErrorCode = ErrorCodes.ArgumentNull;
        //        return false;
        //    }

        //    Entity = extraInnerCircleSketch.Entity;

        //    return true;
        //}
    }
}
