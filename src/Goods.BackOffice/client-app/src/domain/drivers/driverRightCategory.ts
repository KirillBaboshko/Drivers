export enum RightsCategory {
	CategoryM = 1,
	CategoryA = 2,
	CategoryB = 3,
	CategoryC = 4,
	CategoryD = 5
}

export namespace RightsCategory {
	export const getDisplayName = (rightsCategory: RightsCategory): string => {
		switch (rightsCategory) {
			case RightsCategory.CategoryM:
				return 'Мопеды и легкие квадрициклы (M)';
			case RightsCategory.CategoryA:
				return 'Мотоциклы (A)';
            case RightsCategory.CategoryB:
				return 'Легковые автомобили (B)';
            case RightsCategory.CategoryC:
				return 'Грузовые автомобили (C)';
            case RightsCategory.CategoryD:
				return 'Автобусы (D)';
		}
	};
    export const getShortName = (rightsCategory: RightsCategory): string => {
		switch (rightsCategory) {
			case RightsCategory.CategoryM:
				return 'M';
			case RightsCategory.CategoryA:
				return 'A';
			case RightsCategory.CategoryB:
				return 'B';
			case RightsCategory.CategoryC:
				return 'C';
			case RightsCategory.CategoryD:
				return 'D';
		}
	};
}