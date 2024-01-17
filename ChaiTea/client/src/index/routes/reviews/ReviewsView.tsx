import React, { useContext, useEffect, useState } from 'react';
import { IReview } from '../../domain/IReview';
import ReviewRating from './ReviewRating';
import { ReviewService } from '../../services/ReviewService';
import { JwtContext } from '../Root';
import jwtDecode from 'jwt-decode';
import CommentView from '../comments/CommentView';

interface BeverageData {
    beverageId: string
}


const ReviewsView = (beverageData: BeverageData) => {
    const { jwtResponse, setJwtResponse } = useContext(JwtContext);
    const reviewService = new ReviewService(setJwtResponse!, beverageData.beverageId);

    const [validationErrors, setValidationErrors] = useState([] as string[]);
    const [data, setData] = useState([] as IReview[]);

    const [newReviewText, setNewReview] = useState('');
    const [newRatingValue, setNewRating] = useState('');

    const [editReviewId, setEditReviewId] = useState('');
    const [editReviewText, setEditReviewText] = useState('');
    const [editRatingValue, setEditRatingValue] = useState('');
    const [isEditing, setIsEditing] = useState(false);

    let userName = "";
    let userId = "";
    const maxReviewLength = 512;

    useEffect(() => {
        reviewService.getBeverageReview(beverageData.beverageId)
            .then(response => {
                if (response) setData(response);
            }
            );
    }, []);

    if (jwtResponse != null) {
        let decodedJwt: any = jwtDecode(jwtResponse.jwt);

        userId = decodedJwt["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];

        let firstName = decodedJwt['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname'];
        let lastName = decodedJwt['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname'];
        userName = firstName + " " + lastName;
    }

    const handleEditReview = (review: IReview) => {
        if (jwtResponse && review.appUserId === userId) {
            setEditReviewId(review.id!)
            setEditReviewText(review.reviewText!);
            setEditRatingValue(review.rating.toString());
            setIsEditing(true);
        }
    };

    const handleDeleteReview = async (review: IReview) => {
        if (jwtResponse && review.appUserId == userId) {
            let deleteResult = await reviewService.deleteReview(jwtResponse!, review.id!)

            if (deleteResult) {
                console.log('Delete review:', review);
            }
        }
    };

    const handleReviewSubmit = async () => {
        if (isEditing) {

            const editResult = await reviewService.updateReview(jwtResponse!, {
                id: editReviewId,
                beverageId: beverageData.beverageId,
                appUserId: userId,
                name: userName,
                rating: parseFloat(editRatingValue),
                reviewText: editReviewText,
            });

            if (editResult) {
                setEditReviewId('');
                setEditReviewText('');
                setEditRatingValue('');
                console.log('Edit review:', editResult);
            }
        } else {
            let postResult = await reviewService.postReview(jwtResponse!, {
                beverageId: beverageData.beverageId,
                appUserId: userId,
                name: userName,
                rating: parseFloat(newRatingValue),
                reviewText: newReviewText,
            });

            if (postResult) {

                // Reset the input fields after submitting the review
                setNewReview('');
                setNewRating('');
            }
        }

    };

    return (
        <>
            <div className="beverageReviewView">
                <div className="reviewContainer">
                    <label><h5>Rating&nbsp;</h5></label>
                    <input
                        className="reviewRatingField"
                        type="number"
                        min="0.0"
                        max="5.0"
                        step="0.1"
                        value={isEditing ? editRatingValue : newRatingValue}
                        onChange={(e) => {
                            const ratingValue = parseFloat(e.target.value); // Convert the entered value to a float

                            if (!isNaN(ratingValue) && ratingValue >= 0.0 && ratingValue <= 5.0) {
                                if (isEditing) {
                                    setEditRatingValue(e.target.value);
                                } else {
                                    setNewRating(e.target.value);
                                }
                            } else {
                                setValidationErrors(["The rating is out of bound. Keep it between 0-5!"]);
                            }
                        }}
                        placeholder="0-5"
                    />
                    <span className='reviewError' style={{ 'display': validationErrors.length === 0 ? 'none' : '' }}>
                        <span>{validationErrors.length > 0 ? validationErrors[0] : ''}</span>
                    </span>
                    <hr />
                    <input
                        className="reviewTextField"
                        type="text"
                        value={isEditing ? editReviewText : newReviewText}
                        onChange={(e) => {
                            const reviewTextValue = e.target.value;

                            if (reviewTextValue.length <= maxReviewLength) {
                                if (isEditing) {
                                    setEditReviewText(reviewTextValue);
                                } else {
                                    setNewReview(reviewTextValue);
                                }
                            } else {
                                setValidationErrors(["Comment is too long!"]);
                            }
                        }}
                        placeholder="Comment"
                    />
                    <div className="reviewActions">
                        <button className="submitButton" onClick={handleReviewSubmit}>Submit</button>
                    </div>
                </div>
                {data == null
                    ? ''
                    :
                    data.map((review) => ( <>
                        <div className="reviewContainer" key={review.id}>
                            <div>
                                <span className="reviewContainerText">{review.name} </span>
                                <ReviewRating rating={review.rating} />
                            </div>
                            {review.reviewText}
                            {review.appUserId === userId && ( // Check if the review's user ID matches the current user's ID
                                <div className="reviewActions">
                                    <button className="editButton" onClick={() => handleEditReview(review)}>Edit</button>
                                    <button className="deleteButton" onClick={() => handleDeleteReview(review)}>Delete</button>
                                </div>
                            )}
                        </div>
                        <CommentView reviewId={review.id!} />
                        </>))}
            </div>
        </>
    );
};

export default ReviewsView;