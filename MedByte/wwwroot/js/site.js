// Example React Component
class TomografList extends React.Component {
    render() {
        return (
            <div className="row">
                {this.props.tomografy.map(tomograf => (
                    <div className="col-md-4" key={tomograf.id}>
                        <div className="card mb-4 shadow-sm">
                            <div className="card-body">
                                <h5 className="card-title">{tomograf.nazwa}</h5>
                                <p className="card-text">{tomograf.opis}</p>
                                <div className="d-flex justify-content-between align-items-center">
                                    <div className="btn-group">
                                        <a href={`/Tomografy/Edit/${tomograf.id}`} className="btn btn-sm btn-outline-secondary">Edit</a>
                                        <a href={`/Tomografy/Details/${tomograf.id}`} className="btn btn-sm btn-outline-secondary">Details</a>
                                        <a href={`/Tomografy/Delete/${tomograf.id}`} className="btn btn-sm btn-outline-secondary">Delete</a>
                                    </div>
                                    {tomograf.imagePath && <img src={tomograf.imagePath} className="img-thumbnail" alt="Image" width="100" />}
                                </div>
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        );
    }
}

// Fetch Tomografy data and render the React component
document.addEventListener('DOMContentLoaded', function () {
    const container = document.getElementById('tomografy-container');
    if (container) {
        fetch('/Tomografy/GetTomografy')
            .then(response => response.json())
            .then(data => {
                ReactDOM.render(<TomografList tomografy={data} />, container);
            });
    }
});
