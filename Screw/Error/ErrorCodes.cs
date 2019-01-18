namespace Screw.Error
{
    public enum ErrorCodes
    {
        OK = 0,

        // Kompas application and managers
        KompasObjectCreatingError,
        KompasApplicationCreatingError,
        ManagerCreatingError,

        // Function arguments
        ArgumentInvalid,
        ArgumentNull,

        // Detail move errors
        PositionerPrepareError,
        PositionerFinishError,
        PositionerSetAxisError,
        PositionerSetBasePointError,
        PositionerSetDragPointError,
        PositionerMoveComponentError,

        // Document 2D
        Document2DCircleCreatingError,
        Document2DRegPolyCreatingError,
        Document2DRegPolyCreateError,

        // Build manager errors
        Document3DGetPartError,
        Document3DCreateError,

        // Element not set
        KompasFigureNotSet,
        EntityCreateError,
        EntityCollectionCreateError,
        EntityDefinitionNull,
        EntityNull,
        EntityCollectionWrong,

        // Extrusions
        ExtrudableEntityNotSet,
        ExtrusionEntityCreationError,
        ExtrusionTypeCurrentlyNotSupported,
        ExtrusionDirectionNotSupported,
        ExtrusionFacesCountWrong,
        ExtrusionSetLoftParamError,
        ExtrusionSetSideParamError,
        ExtrusionSetSketchError,
        ExtrusionDepthNotSet,
        ExtrudableEntityNull,
        // Sketches definitions
        ExtrusionSketchesNull,
        ExtrusionSketchesNotSet,
        ExtrusionSketchesMoreThanOne,

        // Validations
        DoubleValueValidationError,
        FigureParametersValidationError,
        UserInputValidationError,
    }
}
