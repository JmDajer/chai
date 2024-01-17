import { useContext, useEffect, useState } from "react";
import Multiselect from 'multiselect-react-dropdown';
import { BeverageService } from "../../services/BeverageService";
import { JwtContext } from "../Root";
import { TagService } from "../../services/TagService";
import { IngredientService } from "../../services/IngredientService";
import { IBeverage } from "../../domain/IBeverage";
import { ITag } from "../../domain/ITag";
import { IIngredient } from "../../domain/IIngredient";
import jwtDecode from "jwt-decode";


const CustomBeverageView = () => {
    const [name, setName] = useState<string>('');
    const [description, setDescription] = useState<string>('');
    const [selectedBeverages, setSelectedBeverages] = useState<IBeverage[]>([]);
    const [selectedTags, setSelectedTags] = useState<ITag[]>([]);
    const [selectedIngredients, setSelectedIngredients] = useState<IIngredient[]>([]);

    const [beverageData, setBeverageData] = useState<IBeverage[]>([]);
    const [tagData, setTagData] = useState<ITag[]>([]);
    const [ingredientData, setIngredientData] = useState<IIngredient[]>([]);

    const { jwtResponse, setJwtResponse } = useContext(JwtContext);

    let beverageService = new BeverageService(setJwtResponse!);
    let tagService = new TagService(setJwtResponse!);
    let ingredientService = new IngredientService(setJwtResponse!);

    useEffect(() => {
        fetchIngredients();
        fetchBeverages();
        fetchTags()
    }, [])

    const fetchBeverages = async () => {
        const beverages = await beverageService.getAllBeverages();
        if (beverages) {
            setBeverageData(beverages);
        }
    };

    const fetchIngredients = async () => {
        const ingredients = await ingredientService.getAllIngredients();
        if (ingredients) {
            setIngredientData(ingredients);
        }
    };

    const fetchTags = async () => {
        const tags = await tagService.getAllTags();
        if (tags) {
            setTagData(tags);
        }
    };

    const handleBeverageSubmit = async () => {
        if (jwtResponse) {
            let decodedJwt: any = jwtDecode(jwtResponse.jwt);
            let userId = decodedJwt["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];

            let newSelectedTags = selectedTags.map(tag => ({ ...tag }));

            let beverage: IBeverage = {
                appUserId: userId,
                name: name,
                description: description,
                tags: newSelectedTags,
                ingredients: selectedIngredients,
                parentBeverages: selectedBeverages,
                pictures: []
            }

            console.log(beverage);

            let postResult = await beverageService.postBeverage(jwtResponse!, beverage);

            if (postResult) {
                setName("");
                setDescription("");
                setSelectedTags([]);
                setSelectedIngredients([]);
                setSelectedBeverages([]);
            }

        }
    }

    const handleBeverageChange = (selectedList: IBeverage[]) => {
        setSelectedBeverages(selectedList);
    };

    const handleIngredientChange = (selectedList: IIngredient[]) => {
        setSelectedIngredients(selectedList);
    };

    const handleTagChange = (selectedList: ITag[]) => {
        setSelectedTags(selectedList);
    };

    const multiselectStyles = {
        multiselectContainer: {
            width: '80%',
        }
    };

    return (
        <div className="customFromContainer">
            <div className="customFormContainerInner">
                <h2>Create custom tea</h2>
                <div>
                    <label>Name:
                        <input className="customFromField" type="text" id="name" value={name} onChange={(e) => setName(e.target.value)} />
                    </label>
                </div>
                <div>
                    <label>Description:
                        <input className="customFromField" type="text" id="description" value={description} onChange={(e) => setDescription(e.target.value)} />
                    </label>
                </div>
                <span>Beverages: </span>
                <Multiselect className="customFromField"
                    options={beverageData}
                    selectedValues={selectedBeverages}
                    onSelect={handleBeverageChange}
                    onRemove={handleBeverageChange}
                    displayValue="name"
                    style={multiselectStyles}
                />
                <span>Ingredients: </span>
                <Multiselect className="customFromField"
                    options={ingredientData}
                    selectedValues={selectedIngredients}
                    onSelect={handleIngredientChange}
                    onRemove={handleIngredientChange}
                    displayValue="name"
                    style={multiselectStyles}
                />
                <span>Tags: </span>
                <Multiselect className="customFromField"
                    options={tagData}
                    selectedValues={selectedTags}
                    onSelect={handleTagChange}
                    onRemove={handleTagChange}
                    displayValue="name"
                    style={multiselectStyles}
                />
                <button className="submitButton customSubmitButton" onClick={handleBeverageSubmit}>Submit</button>
            </div>
        </div>
    );
};

export default CustomBeverageView;
