using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Screw.Error
{
   public enum ErrorCodes
    {
        OK = 0,
		
		// Ошибки приложения Компаса и менеджера
		KompasObjectCreatingError,
		KompasApplicationCreatingError,
		ManagerCreatingError,

		// агрументы функции
		ArgumentInvalid,
		ArgumentNull,

		//// Detail move errors
		//PositionerPrepareError,
		//PositionerFinishError,
		//PositionerSetAxisError,
		//PositionerSetBasePointError,
		//PositionerSetDragPointError,
		//PositionerMoveComponentError,

		// Ошибки построения 2D фигур
		Document2DCircleCreatingError, // круг
		Document2DRegPolyCreatingError, // многоугольник
		Document2DRectangleCreateError, // прямоугольник
		
		// ошибки построения
		Document3DGetPartError,
		Document3DCreateError,

		// элемент не установлен
		KompasFigureNotSet,
		EntityCreateError, // ошибка создания объекта
		EntityCollectionCreateError, // ошибка создания коллекций сущностей
		EntityDefinitionNull,
		EntityNull,
		EntityCollectionWrong,

		// Выдавливание
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

		// эскизы
		ExtrusionSketchesNull,
		ExtrusionSketchesNotSet,
		ExtrusionSketchesMoreThanOne,

		// Validations
		DoubleValueValidationError,
		FigureParametersValidationError,
		UserInputValidationError,
    }
}
