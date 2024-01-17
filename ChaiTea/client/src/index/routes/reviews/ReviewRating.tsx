interface StarRatingProps {
    rating: number
}

function ReviewRating({ rating }: StarRatingProps) {

    let filledStars = Math.ceil(rating);
    let remainingStar = 5 - filledStars;

    const filledStarElements = Array.from({ length: filledStars }, (_, index) => (
        <span key={index} className="filledStar">&#9733;</span>
    ));

    const remainingStarElements = Array.from({ length: remainingStar }, (_, index) => (
        <span key={index + filledStars} className="emptyStar">&#9733;</span>
    ));

    return (
        <span className="containerRating reviewContainerText">
            <span className="rating reviewContainerStars">{rating}</span>
            <span className="star reviewContainerStars">
                {filledStarElements}
                {remainingStarElements}
            </span>
        </span>
    );
}

export default ReviewRating;